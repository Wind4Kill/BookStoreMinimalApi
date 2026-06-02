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

        Task<Book> GetBookById(int id);
    }
}