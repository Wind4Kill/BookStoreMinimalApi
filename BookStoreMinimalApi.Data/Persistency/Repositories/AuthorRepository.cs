using BookStoreMinimalApi.Application.Interfaces.Repositories;
using BookStoreMinimalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMinimalApi.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        readonly ApplicationContext _context;

        public AuthorRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Author?> GetAuthorById(int id, CancellationToken cancellationToken)
        {
            Author? requestedAuthor = await _context.Authors.
            SingleOrDefaultAsync(a => a.AuthorId == id, cancellationToken);

            return requestedAuthor;
        }

        public async Task<Author?> GetAuthorByName(string authorName, CancellationToken cancellationToken)
        {
            Author? requestedAuthor = await _context.Authors.
            SingleOrDefaultAsync(a => a.Name == authorName, cancellationToken);

            return requestedAuthor;
        }
    }
}