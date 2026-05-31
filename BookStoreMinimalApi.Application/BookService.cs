using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
using BookStoreMinimalApi.Domain.Interfaces.Services;

namespace BookStoreMinimalApi.Application
{
    public class BookService : IBookService
    {
        readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<GetBookDTO>> GetAllBooks()
        {
            List<GetBookDTO> filteredBooks = await _bookRepository.GetAllBooks().Select(b =>
            new GetBookDTO
            {
                Title = b.Title,
                Cost = b.Cost
            }).ToAsyncEnumerable().ToListAsync();

            return filteredBooks;

        }
    }
}