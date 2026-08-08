using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.Entities;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;

namespace BookStoreMinimalApi.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        readonly ApplicationContext _context;

        public ReviewRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Review> AddReview(Review review, CancellationToken cancellationToken)
        {
            _context.Set<Review>().Add(review);
            await _context.SaveChangesAsync(cancellationToken);
            return review;
        }
    }
}