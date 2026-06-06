using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Domain.DTOs
{
    public class GetBookByIdDTO
    {
        public required string Title { get; set; }

        public decimal Cost { get; set; }

        public required string Description { get; set; }

        public required string AuthorName { get; set; }

    }
}