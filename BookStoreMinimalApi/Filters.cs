using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.FiltrationEntities;

namespace BookStoreMinimalApi.Endpoints
{
    public class Filters : IValidatableObject
    {
        public string? FilterOptions { get; set; }

        public string? OrderOptions { get; set; }

        public string? FilterValue { get; set; }

        public int? PageNum { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PageNum < 0)
            {
                yield return new ValidationResult("Page number can't be less than zero.",
                new[] { nameof(PageNum) });
            }
        }
    }
}