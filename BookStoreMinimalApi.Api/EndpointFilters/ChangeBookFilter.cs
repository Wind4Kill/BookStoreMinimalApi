using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Api.Validators;
using BookStoreMinimalApi.Application.Books.DTOs.BookDTOs;
using FluentValidation;

namespace BookStoreMinimalApi.Api.EndpointFilters
{
    public class ChangeBookFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            ChangeBookDto changeBookDto = context.Arguments.OfType<ChangeBookDto>().First();

            var validationResult = await changeBookDto.InlineValidateAsync(val =>
            {
                val.RuleFor(dto => dto.Title).MaximumLength(80);
                val.RuleFor(dto => dto.Cost).GreaterThan(0);
                val.RuleFor(dto => dto.Description).MaximumLength(200);
            });

            if (!validationResult.IsValid)
            {
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }

            return await next(context);
        }
    }
}