using AutoMapper;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Application.Reviews.DTOs
{
    [AutoMap(typeof(Review), ReverseMap =true)]
    public class ReviewDto
    {
        public double Rating { get; set; }
        public string? Description { get; set; }
    }
}