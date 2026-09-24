using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStoreMinimalApi.Application.Authorization;
using BookStoreMinimalApi.Application.Authorization.DTOs;
using BookStoreMinimalApi.Application.Users;
using BookStoreMinimalApi.Application.Users.DTOs;
using BookStoreMinimalApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BookStoreMinimalApi.Application.Interfaces.Services
{
    public interface IUserService
    {
         Task RegisterUser(UserRegisterDTO userCredentials, CancellationToken cancellationToken);
         Task<GetTokensDTO> Login(UserLoginDTO userCredentials);

         Task AddReviewToUser(ClaimsPrincipal claims, Review review);
         Task<GetTokensDTO> RefreshAccessToken(string token);
    }
     
}