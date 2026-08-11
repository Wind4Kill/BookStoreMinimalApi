using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Api.Validators;
using BookStoreMinimalApi.Domain.DTOs.AuthorDTOs;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.DTOs.CategoryDTOs;
using FluentValidation;

namespace BookStoreMinimalApi.Api.EndpointFilters
{
    public class CreateBookFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            CreateBookDto createdBook = context.Arguments.OfType<CreateBookDto>().Single();
            var createAuthorValidator = new InlineValidator<CreateAuthorDto>();
            createAuthorValidator.RuleFor(a => a.Name).NotEmpty().WithMessage("Author name field can't be empty.").
            MaximumLength(70).WithName("Maximum name field length can't exceed 70 symbols.");
            createAuthorValidator.RuleFor(a => a.DateOfBirth).NotEmpty().WithMessage("Birthday field can't be empty.");

            var categoryDtoValidator = new InlineValidator<CategoryDTO>();
            categoryDtoValidator.RuleFor(c => c.CategoryName).NotEmpty().WithMessage("Category name field can't be empty.").
            MaximumLength(50).WithMessage("Category name field length can't exceed 50 symbols.");

            var validationResult = await createdBook.InlineValidateAsync((v) =>
            {
                v.RuleFor(b => b.Title).NotEmpty().MaximumLength(80);
                v.RuleFor(b => b.Description).NotEmpty().MaximumLength(200);
                v.RuleFor(b => b.Cost).NotEmpty().GreaterThan(0);
                v.RuleFor(b => b.Author).NotNull().SetValidator(createAuthorValidator);
                v.RuleForEach(b => b.Categories).NotEmpty().SetValidator(categoryDtoValidator);
            });

            if (!validationResult.IsValid)
            {
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }

            return await next(context);
        }
    }
}