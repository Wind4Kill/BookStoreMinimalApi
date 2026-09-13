
using BookStoreMinimalApi.Application.Interfaces.Repositories;
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Application.Users.DTOs;
using BookStoreMinimalApi.Domain.Exceptions.Users;
using Microsoft.AspNetCore.Identity;

namespace BookStoreMinimalApi.Application.Users.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterUser(UserRegisterDTO userCredentials)
        {
            var createdUser = new IdentityUser() { Email = userCredentials.Email };

            await _userRepository.RegisterUser(createdUser, userCredentials.Password);
            
        }

    }
}