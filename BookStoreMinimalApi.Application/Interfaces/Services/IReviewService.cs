using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStoreMinimalApi.Application.Reviews.DTOs;
using BookStoreMinimalApi.Domain.DTOs;

namespace BookStoreMinimalApi.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task<ReviewDto> AddReview(int bookId, ReviewDto reviewDto,ClaimsPrincipal claims, CancellationToken cancellationToken);
    }
}