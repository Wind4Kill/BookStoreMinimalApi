
using BookStoreMinimalApi.Application.Interfaces.Repositories;
using BookStoreMinimalApi.Domain.Entities;

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