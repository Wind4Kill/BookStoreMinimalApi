using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BookStoreMinimalApi.Application.DTOs.AuthorDTOs
{
    public class CreateAuthorDto
    {
        public required string Name { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}