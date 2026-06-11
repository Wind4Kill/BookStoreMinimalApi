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

        public async Task<Book> AddBook(Book book)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<int> DeleteBook(int id)
        {
            Book requestedBook = _context.Books.Single(b => b.BookId == id);
            requestedBook.IsDeleted = true;
            return await _context.SaveChangesAsync();
        }

        public IQueryable<Book> GetAllBooks()
        {
            return _context.Books.AsNoTracking().Include(b=>b.Author);
        }

        public async Task<Book?> GetBookById(int id)
        {
            return _context.Books.Include(b => b.Author).SingleOrDefault(b => b.BookId == id);
        }

        public async Task<List<T>> ToListAsync<T>(IQueryable<T> query)
        {
            return await query.ToListAsync();
        }
    }
}