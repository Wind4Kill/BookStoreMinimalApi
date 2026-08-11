using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.DTOs.AuthorDTOs;
using BookStoreMinimalApi.Domain.DTOs.CategoryDTOs;

namespace BookStoreMinimalApi.Domain.DTOs.BookDTOs
{
    public class CreateBookDto
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public int Cost { get; set; }

        public required CreateAuthorDto Author { get; set; }

        public required List<CategoryDTO> Categories { get; set; }
    }
}