using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMinimalApi.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        readonly ApplicationContext _context;
        public BookRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Book> AddBook(Book book, CancellationToken cancellationToken)
        {
            _context.Add(book);
            await _context.SaveChangesAsync(cancellationToken);
            return book;
        }

        public async Task DeleteBook(Book book, CancellationToken cancellationToken)
        {
            book.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<Book> GetAllBooks()
        {
            return _context.Books.AsSplitQuery()
            .AsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.Categories);
        }

        public async Task<Book?> GetBookById(int id, CancellationToken cancellationToken)
        {
            return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Categories)
            .Include(b => b.Reviews)
            .SingleOrDefaultAsync(b => b.BookId == id, cancellationToken);
        }
        public async Task UpdateBook(CancellationToken cancellationToken)
        {
             await _context.SaveChangesAsync(cancellationToken);
        }
    }
}