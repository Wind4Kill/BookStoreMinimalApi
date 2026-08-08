using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Domain.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        Task<Review> AddReview(Review review, CancellationToken cancellationToken);
    }
}