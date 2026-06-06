using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Domain.DTOs.AuthorDTOs
{
    public class CreateAuthorDto
    {
        public required string Name { get; set; }
    }
}