using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.DTOs;

namespace BookStoreMinimalApi.Domain.Interfaces.Services
{
    public interface IReviewService
    {
        Task<int> AddReview(ReviewDto reviewDto);
    }
}