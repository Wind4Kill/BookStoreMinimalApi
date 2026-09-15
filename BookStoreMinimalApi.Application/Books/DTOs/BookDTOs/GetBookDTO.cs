using AutoMapper;
using BookStoreMinimalApi.Application.Categories.DTOs.CategoryDTOs;
using BookStoreMinimalApi.Data;

namespace BookStoreMinimalApi.Application.Books.DTOs.BookDTOs
{
    [AutoMap(typeof(Book))]
        public class GetBookDTO
    {
        public int BookId { get; set; }
        public required string Title { get; set; }
        public decimal Cost { get; set; }
        public required string AuthorName { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; } = null!;
    }
}