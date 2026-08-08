using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Domain.Interfaces.Services
{
    public interface IReviewService
    {
        Task<ReviewDto> AddReview(int bookId, ReviewDto reviewDto, CancellationToken cancellationToken);
    }
}