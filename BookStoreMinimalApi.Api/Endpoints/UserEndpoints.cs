using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Api.EndpointFilters;
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Application.Users.DTOs;

namespace BookStoreMinimalApi.Api.Endpoints
{
    public static class UserEndpoints
    {
        public static void AddUserEndpoints(this WebApplication app)
        {
            var userEndpoints = app.MapGroup("api/users").WithTags("Users");

            userEndpoints.MapPost("register", async (UserRegisterDTO userCredentials, IUserService userService) =>
            {
                await userService.RegisterUser(userCredentials);
                return Results.Created();

            }).AddEndpointFilter<UserRegisterFilter>()
            .Produces(200).ProducesValidationProblem();
            
            

        }
    }
}