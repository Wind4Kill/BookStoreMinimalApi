using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.Entities;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
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
        public async Task<Author?> GetAuthorById(int id)
        {
            Author? requestedAuthor = await _context.Authors.SingleOrDefaultAsync(a => a.AuthorId == id);
            return requestedAuthor;
        }

        public async Task<Author?> GetAuthorByName(string authorName)
        {
            Author? requestedAuthor = await _context.Authors.SingleOrDefaultAsync(a => a.Name == authorName);
            return requestedAuthor;
        }
    }
}