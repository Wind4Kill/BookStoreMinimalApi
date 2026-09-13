using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Application.Interfaces.Repositories;
using BookStoreMinimalApi.Application.Users.DTOs;
using BookStoreMinimalApi.Data.Migrations;
using BookStoreMinimalApi.Domain.Exceptions.Users;
using Microsoft.AspNetCore.Identity;

namespace BookStoreMinimalApi.Data.Persistency.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly UserManager<IdentityUser> _userManager;
        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task RegisterUser(IdentityUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if(!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors);
                throw new UserRegisterValidationException(errors);
            }
        }

        public async Task<IdentityUser?> GetUserByEmail(string email)
        {
            var requestedUser = await _userManager.FindByEmailAsync(email);
            return requestedUser;
        }
        
        
    }
}