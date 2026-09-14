using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Domain.Exceptions.Users;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMinimalApi.Api
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (statusCode, message) = exception switch
            {
                EntityNotFoundException => (StatusCodes.Status404NotFound, "Entity Not Found."),
                UserCredentialsValidationException => (StatusCodes.Status400BadRequest, "Register validation data invalid."),
                UserNotFoundException => (StatusCodes.Status404NotFound, "Requested user wasn't found."),
                _ => (StatusCodes.Status500InternalServerError, "Interal Server Error")
            };

            ProblemDetails details = new ProblemDetails
            {
                Title = message,
                Status = statusCode,
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);
            return true;
        }
    }
}