using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Api.Validators;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.DTOs.AuthorDTOs;
using FluentValidation;

namespace BookStoreMinimalApi.Api.EndpointFilters
{
    public class CreateReviewFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            ReviewDto createdReview = context.Arguments.OfType<ReviewDto>().Single();
            var validationResult = await createdReview.InlineValidateAsync((v) =>
            {
                v.RuleFor(r => r.Rating).NotEmpty().InclusiveBetween(1, 5).WithMessage("Rating must be in 1 to 5 points.");
                v.RuleFor(r => r.Description).MaximumLength(200).WithMessage("Review text can't exceed 200 symbols.");
            });

            if (!validationResult.IsValid)
            {
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }

            return await next(context);
        }
    }
}