using AutoMapper;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs.CategoryDTOs;

namespace BookStoreMinimalApi.Domain.DTOs.BookDTOs
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