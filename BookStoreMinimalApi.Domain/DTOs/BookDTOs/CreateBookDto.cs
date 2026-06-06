using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.DTOs.AuthorDTOs;

namespace BookStoreMinimalApi.Domain.DTOs.BookDTOs
{
    public class CreateBookDto
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public int Cost { get; set; }

        public required CreateAuthorDto Author { get; set; }
    }
}