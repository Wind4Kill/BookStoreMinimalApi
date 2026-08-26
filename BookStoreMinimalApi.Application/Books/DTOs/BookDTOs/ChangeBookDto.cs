using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Application.Books.DTOs.BookDTOs
{
    public class ChangeBookDto
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public decimal? Cost { get; set; }
    }
}