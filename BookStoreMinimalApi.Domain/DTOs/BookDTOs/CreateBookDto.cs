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
        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public required CreateAuthorDto Author { get; set; }

        [Required] 
        public required List<CategoryDTO> Categories { get; set; }
    }
}