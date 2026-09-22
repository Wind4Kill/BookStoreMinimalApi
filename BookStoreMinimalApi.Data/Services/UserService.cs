
using System.Security.Claims;
using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Application.Interfaces.Abstractions.Authorization;
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Application.Users.DTOs;
using BookStoreMinimalApi.Domain.Entities;
using BookStoreMinimalApi.Domain.Exceptions.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMinimalApi.Data.Services
{
    public class UserService : IUserService
    {
        readonly ApplicationContext _dbContext;
        readonly UserManager<User> _userManager;
        readonly ITokenProvider _tokenProvider;

        public UserService(ApplicationContext dbContext, UserManager<User> userManager, ITokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _tokenProvider = tokenProvider;
        }

        public async Task<string> Login(UserLoginDTO userCredentials)
        {
            var requestedUser = await _userManager.FindByEmailAsync(userCredentials.Email);

            if (requestedUser is null)
            {
                throw new UserNotFoundException("Requested user wasn't found.");
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(requestedUser, userCredentials.Password);

            if (!isPasswordValid)
            {
                throw new UserCredentialsValidationException("Provided user data is wrong.");
            }

            var claims = (await _userManager.GetClaimsAsync(requestedUser)).ToList();

            string token = _tokenProvider.GenerateToken(requestedUser, claims);


            return token;

        }

        public async Task RegisterUser(UserRegisterDTO userCredentials, CancellationToken cancellationToken)
        {
            User createdUser = new User(userCredentials.Login) { Email = userCredentials.Email };

            List<Claim> claims = new()
            {
                new Claim("Role", "User")
            };

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                var result = await _userManager.CreateAsync(createdUser, userCredentials.Password);

                if (!result.Succeeded)
                {
                    string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new UserCredentialsValidationException(errorMessage);
                }
                await _userManager.AddClaimsAsync(createdUser, claims);

                await transaction.CommitAsync(cancellationToken);

            });

        }

        public async Task AddReviewToUser(ClaimsPrincipal claims, Review review)
        {
            User? requestedUser = await _userManager.GetUserAsync(claims);
            if (requestedUser is null)
            {
                throw new EntityNotFoundException("User wasn't found.");
            }
            await _dbContext.Entry(requestedUser).Collection(ru => ru.Reviews).LoadAsync();
            requestedUser.Reviews.Add(review);
        }
    }
}