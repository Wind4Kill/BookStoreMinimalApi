
using BookStoreMinimalApi.Api.Validators;
using BookStoreMinimalApi.Application.Users;
using FluentValidation;

namespace BookStoreMinimalApi.Api.EndpointFilters
{
    public class UserLoginFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            UserLoginDTO userCredentials = context.Arguments.OfType<UserLoginDTO>().First();
            var validationResult = await userCredentials.InlineValidateAsync(val =>
            {
                val.RuleFor(u => u.Email).NotEmpty().WithMessage("User password can't be empty.")
                .EmailAddress().WithMessage("Email address has an inappropriate format.");
                val.RuleFor(u => u.Password).NotEmpty().WithMessage("User password can't be empty")
                .MinimumLength(8).WithMessage("Password must be not least than 8 symbols in length.");
            });

            if (!validationResult.IsValid)
            {
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }

            return await next(context);
        }
    }
}