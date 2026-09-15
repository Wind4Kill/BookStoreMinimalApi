using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Application.Authorization;
using BookStoreMinimalApi.Application.Users;
using BookStoreMinimalApi.Application.Users.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BookStoreMinimalApi.Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task RegisterUser(UserRegisterDTO userCredentials);
        public Task<string> Login(UserLoginDTO userCredentials);

        

    }
     
}