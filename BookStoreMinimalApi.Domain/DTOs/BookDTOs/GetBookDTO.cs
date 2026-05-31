using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Domain.DTOs.BookDTOs
{
    public class GetBookDTO
    {
        public string Title { get; set; } = null!;
        public decimal Cost { get; set; }
    }
}