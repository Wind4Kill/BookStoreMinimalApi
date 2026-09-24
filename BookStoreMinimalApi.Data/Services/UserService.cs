
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Transactions;
using BookStoreMinimalApi.Application.Authorization.DTOs;
using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Application.Interfaces.Abstractions.Authorization;
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Application.Users.DTOs;
using BookStoreMinimalApi.Domain.Entities;
using BookStoreMinimalApi.Domain.Entities.User;
using BookStoreMinimalApi.Domain.Exceptions.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMinimalApi.Data.Services
{
    public class UserService : IUserService
    {
        readonly IHttpContextAccessor _httpContext;
        readonly ApplicationContext _dbContext;
        readonly UserManager<User> _userManager;
        readonly ITokenProvider _tokenProvider;

        public UserService(IHttpContextAccessor httpContext, ApplicationContext dbContext, UserManager<User> userManager, ITokenProvider tokenProvider)
        {
            _httpContext = httpContext;
            _dbContext = dbContext;
            _userManager = userManager;
            _tokenProvider = tokenProvider;
        }

        public async Task<GetTokensDTO> Login(UserLoginDTO userCredentials)
        {
            User? requestedUser = await _userManager.FindByEmailAsync(userCredentials.Email);

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

            await _dbContext.Entry(requestedUser).Collection(u => u.RefreshTokens).LoadAsync();
            if (requestedUser.RefreshTokens is not null)
            {
                _dbContext.RefreshTokens.RemoveRange(requestedUser.RefreshTokens);
            }

            string accessToken = _tokenProvider.GenerateAccessToken(requestedUser, claims);

            RefreshToken refreshToken = new RefreshToken()
            {
                RefreshTokenId = Guid.NewGuid().ToString(),
                ExpirationUtc = DateTime.UtcNow.AddDays(3),
                Token = _tokenProvider.GenerateRefreshToken(),
                User = requestedUser
            };

            _dbContext.RefreshTokens.Add(refreshToken);

            await _dbContext.SaveChangesAsync();

            GetTokensDTO tokens = new GetTokensDTO()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };

            return tokens;
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

        public async Task<GetTokensDTO> RefreshAccessToken(string token)
        {
            RefreshToken? refreshToken = await _dbContext.RefreshTokens.Include(r => r.User).SingleAsync(r => r.Token == token);
            if (refreshToken is null || refreshToken.ExpirationUtc < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Token expired.");
            }

            if (!CheckUserId(refreshToken.UserId))
            {
                throw new InvalidOperationException("Operation is not permitted.");
            }

            var claims = (await _userManager.GetClaimsAsync(refreshToken.User)).ToList();
            string accessToken = _tokenProvider.GenerateAccessToken(refreshToken.User, claims);
            refreshToken.Token = _tokenProvider.GenerateRefreshToken();
            refreshToken.ExpirationUtc = DateTime.UtcNow.AddDays(7);

            GetTokensDTO tokens = new()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };

            return tokens;
        }

        private bool CheckUserId(string userId)
        {
            bool isMatch = _httpContext!.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier) == userId ? true : false;
            return isMatch;
        }
    }
}