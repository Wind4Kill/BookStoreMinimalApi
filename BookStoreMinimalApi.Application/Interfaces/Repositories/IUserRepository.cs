using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Application.Users.DTOs;
using Microsoft.AspNetCore.Identity;

namespace BookStoreMinimalApi.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task RegisterUser(IdentityUser user,  string password);

        public Task<IdentityUser?> GetUserByEmail(string email);
    }
}