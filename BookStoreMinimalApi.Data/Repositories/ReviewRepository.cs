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
        public Task<int> AddReview(Review review)
        {
            _context.Set<Review>().Add(review);
            return _context.SaveChangesAsync();
        }
    }
}