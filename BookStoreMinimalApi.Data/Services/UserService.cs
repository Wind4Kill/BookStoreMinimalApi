
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Application.Users;
using BookStoreMinimalApi.Application.Users.DTOs;
using BookStoreMinimalApi.Domain.Exceptions.Users;
using Microsoft.AspNetCore.Identity;

namespace BookStoreMinimalApi.Data.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Login(UserLoginDTO userCredentials)
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
            

        }

        public async Task RegisterUser(UserRegisterDTO userCredentials)
        {
            IdentityUser createdUser = new IdentityUser(userCredentials.Login) { Email = userCredentials.Email };

            var result = await _userManager.CreateAsync(createdUser, userCredentials.Password);

            if (!result.Succeeded)
            {
                string errorMessage = string.Join(", ", result.Errors.Select(e=>e.Description));
                throw new UserCredentialsValidationException(errorMessage);
            }
        }
    }
}