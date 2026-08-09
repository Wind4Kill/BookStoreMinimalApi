using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Api.Validators;
using BookStoreMinimalApi.Domain.FiltrationEntities;
using BookStoreMinimalApi.Endpoints;
using FluentValidation;

namespace BookStoreMinimalApi.Api.EndpointFilters
{
    public class FiltrationEndpointFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            Filters filterOptions = context.Arguments.OfType<Filters>().First();

            var validationResult = await filterOptions.InlineValidateAsync(val =>
            {
                val.RuleFor(fo => fo.PageNum).GreaterThanOrEqualTo(1).WithMessage("Page number can't be less than 1.");
                val.RuleFor(fo => fo.FilterOptions).Must((filterOptions, filterType) =>
                {
                    if (filterType is not null && filterType != FilterOptions.None.ToString() && filterOptions.FilterValue is null)
                        return false;
                    else
                        return true;
                }).WithMessage("Filtration type other from default must be provided along with filtration value.");
            });

            if (!validationResult.IsValid)
            {
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }

            return await next(context);
        }
    }
}