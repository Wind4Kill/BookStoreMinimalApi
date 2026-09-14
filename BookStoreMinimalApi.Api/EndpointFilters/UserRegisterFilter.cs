
using BookStoreMinimalApi.Api.Validators;
using BookStoreMinimalApi.Application.Users.DTOs;
using FluentValidation;

namespace BookStoreMinimalApi.Api.EndpointFilters
{
    public class UserRegisterFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            UserRegisterDTO userCredentials = context.Arguments.OfType<UserRegisterDTO>().First();

            var validationResult = await userCredentials.InlineValidateAsync(val =>
            {
                val.RuleFor(u => u.Email).NotEmpty().WithMessage("Email address can't be empty.")
                .EmailAddress().WithMessage("Email address has an inappropriate format.");
                val.RuleFor(u => u.Password).NotEmpty().WithMessage("User password can't be empty.")
                .MinimumLength(8).WithMessage("Password must be not less than 8 symbols in length.");
                val.RuleFor(u => u.Login).NotEmpty().WithMessage("Login can't be empty.")
                .MinimumLength(8).WithMessage("Login can't be shorter than 15 symbols.")
                .MaximumLength(30).WithMessage("Login length can't exceed 30 symbols.");
            });

            if (!validationResult.IsValid)
            {

                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }
            return await next(context);
        }
    }
}