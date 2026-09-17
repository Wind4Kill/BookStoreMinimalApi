using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using BookStoreMinimalApi.Application.Books.FiltrationEntities;
using BookStoreMinimalApi.Application.Interfaces.Repositories;
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
            return book;
        }

        public async Task DeleteBook(Book book)
        {
            book.IsDeleted = true;
        }

        public async Task<List<Book>> GetAllBooks(Filtration filterOptions, CancellationToken cancellationToken)
        {
            IQueryable<Book> orderedBooks = OrderEntities(_context.Books.AsSplitQuery()
            .AsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.Categories)
            .Include(b=>b.Reviews), filterOptions.OrderOptions);

            IQueryable<Book> filteredBooks = FilterEntities(orderedBooks, filterOptions.FilterOptions,
            filterOptions.FilterValue!);

            IQueryable<Book> paginatedBooks = Paginate(filteredBooks, filterOptions.PageNum);

            List<Book> requestedBooks = await paginatedBooks.ToListAsync();

            return requestedBooks;
            
        }

        public async Task<Book?> GetBookById(int id, CancellationToken cancellationToken)
        {
            return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Categories)
            .Include(b => b.Reviews)
            .SingleOrDefaultAsync(b => b.BookId == id, cancellationToken);
        }
         private  IQueryable<Book> FilterEntities( IQueryable<Book> books, FilterOptions options, string filterValue)
        {
            return options switch
            {
                FilterOptions.None => books,
                FilterOptions.ByCost => books.Where(b => b.Cost <= decimal.Parse(filterValue)),
                FilterOptions.ByCategory => books.Where(b => b.Categories.Select(c => c.CategoryName).Contains(filterValue)),
                _ => books
            };
        }

        private  IQueryable<Book> OrderEntities( IQueryable<Book> books, OrderOptions options)
        {
            return options switch
            {
                OrderOptions.ByDefault => books.OrderBy(b => b.BookId),
                OrderOptions.ByCost => books.OrderBy(b => b.Cost),
                OrderOptions.ByTitle => books.OrderBy(b => b.Title),
                _ => books.OrderBy(b => b.BookId)
            };
        }

        private  IQueryable<Book> Paginate( IQueryable<Book> books, int pageNum)
        {
            if (pageNum < 1)
            {
                throw new ArgumentException("Page number can't be less than 1");
            }

            return books.Skip((pageNum - 1) * 10).Take(10);
        }
    }
}