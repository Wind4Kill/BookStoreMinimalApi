using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Domain.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }

        public double Rating { get; set; }

        public string? Description { get; set; }
        
        public int BookId { get; set; }
    }
}