using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;

namespace BookStoreMinimalApi.Domain.Interfaces.Repositories
{
    public interface IBookRepository
    {
        IQueryable<Book> GetAllBooks();

        Task<List<T>> ToListAsync<T>(IQueryable<T> query);

        Task<Book?> GetBookById(int id);

        Task<Book> AddBook(Book book);

        Task<int> DeleteBook(int id);
        
    }
}