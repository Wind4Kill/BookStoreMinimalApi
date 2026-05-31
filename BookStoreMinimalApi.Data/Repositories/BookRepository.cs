using System;
using System.Collections.Generic;
using System.Linq;
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
        public  IQueryable<Book> GetAllBooks()
        {
            return  _context.Books;
            
        }
    }
}