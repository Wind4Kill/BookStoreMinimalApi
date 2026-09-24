using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Api.EndpointFilters;
using BookStoreMinimalApi.Application.Authorization;
using BookStoreMinimalApi.Application.Authorization.DTOs;
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Application.Users;
using BookStoreMinimalApi.Application.Users.DTOs;
using Microsoft.Extensions.Options;

namespace BookStoreMinimalApi.Api.Endpoints
{
    public static class UserEndpoints
    {
        public static void AddUserEndpoints(this WebApplication app)
        {
            var userEndpoints = app.MapGroup("api/users").WithTags("Users");

            userEndpoints.MapPost("register", async (UserRegisterDTO userCredentials, IUserService userService, CancellationToken cancellationToken) =>
            {
                await userService.RegisterUser(userCredentials, cancellationToken);
                return Results.Ok();

            }).AddEndpointFilter<UserRegisterFilter>()
            .Produces(200).ProducesValidationProblem();

            userEndpoints.MapPost("login", async (UserLoginDTO userCredentials, IUserService userService) =>
            {
                GetTokenDTO tokens = await userService.Login(userCredentials);
                return Results.Ok(tokens);
            }).AddEndpointFilter<UserLoginFilter>().Produces<GetTokenDTO>(200).ProducesValidationProblem();
        }
    }
}