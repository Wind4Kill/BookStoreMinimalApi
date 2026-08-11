using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Domain.DTOs
{
    [AutoMap(typeof(Review), ReverseMap = true)]
    public class ReviewDto
    {
        public double Rating { get; set; }
        public string? Description { get; set; }
    }
}