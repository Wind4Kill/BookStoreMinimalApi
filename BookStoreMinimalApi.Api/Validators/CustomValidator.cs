using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace BookStoreMinimalApi.Api.Validators
{
    public static class CustomValidator
    {
        public static Task<FluentValidation.Results.ValidationResult> InlineValidateAsync<T>(this T obj, Action<InlineValidator<T> >configure)
        {
            var validator = new InlineValidator<T>();
            configure(validator);
            return validator.ValidateAsync(obj); 
        }
    }
} 