using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreMinimalApi.Application.Categories.DTOs.CategoryDTOs;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Application.Books.DTOs.BookDTOs
{
    public class GetBookByIdDTO
    {
        public int BookId { get; set; }
        public required string Title { get; set; }

        public decimal Cost { get; set; }

        public required string Description { get; set; }

        public required string AuthorName { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; } = null!;

        public ICollection<Review>? Reviews { get; set; }

    }
}