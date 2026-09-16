using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Application.Books.FiltrationEntities;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;

namespace BookStoreMinimalApi.Application.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks(Filtration filterOptions, CancellationToken cancellationToken);

        Task<Book?> GetBookById(int id, CancellationToken cancellationToken);

        Task<Book> AddBook(Book book, CancellationToken cancellationToken);

        Task DeleteBook(Book book, CancellationToken cancellationToken);
        
    }
}