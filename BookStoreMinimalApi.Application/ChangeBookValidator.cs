using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;

namespace BookStoreMinimalApi.Application
{
    public class ChangeBookValidator : IValidator<ChangeBookDto>
    {
        public Dictionary<string, string> Validate(ChangeBookDto forValidation)
        {
            Dictionary<string, string> validatedValues = new();
            if (!string.IsNullOrEmpty(forValidation.Title) && forValidation.Title != " ")
            {
                validatedValues[$"{nameof(forValidation.Title)}"] = forValidation.Title;
            }
            if (!string.IsNullOrEmpty(forValidation.Description) && forValidation.Description != " ")
            {
                validatedValues[$"{nameof(forValidation.Description)}"] = forValidation.Description;
            }
            if (forValidation.Cost is not null && forValidation.Cost.Value > 0)
            {
                validatedValues[$"{nameof(forValidation.Cost)}"] = Convert.ToString(forValidation.Cost.Value);
            }

            return validatedValues;
        }
    }
}