using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Domain.DTOs
{
    [AutoMap(typeof(Review))]
    public class ReviewDto
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public double Rating { get; set; }
        public string? Description { get; set; }
    }
}