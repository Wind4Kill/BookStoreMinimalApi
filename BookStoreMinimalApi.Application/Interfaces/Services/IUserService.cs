using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Application.Users.DTOs;

namespace BookStoreMinimalApi.Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task RegisterUser(UserRegisterDTO userCredentials);

    }
     
}