using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Application.Interfaces.Services;
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

        public async Task RegisterUser(UserRegisterDTO userCredentials)
        {
            IdentityUser createdUser = new IdentityUser(userCredentials.Login) { Email = userCredentials.Email };

            var result = await _userManager.CreateAsync(createdUser, userCredentials.Password);

            if (!result.Succeeded)
            {
                string errorMessage = string.Join(", ", result.Errors);
                throw new UserRegisterValidationException(errorMessage);
            }
        }
    }
}