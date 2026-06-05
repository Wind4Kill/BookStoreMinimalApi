using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Domain.DTOs.BookDTOs
{
    public class GetBookDTO
    {
        public int BookId { get; set; }
        public required string Title { get; set; }
        public decimal Cost { get; set; }
    }
}