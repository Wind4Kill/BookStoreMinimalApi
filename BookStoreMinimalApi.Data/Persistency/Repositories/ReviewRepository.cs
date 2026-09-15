
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
        
    }
}