using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Domain.DTOs
{
    public class ReviewDto
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public double Rating { get; set; }
        public string? Description { get; set; }
    }
}