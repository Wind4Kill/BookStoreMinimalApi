using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs;
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
                BookId = b.BookId,
                Title = b.Title,
                Cost = b.Cost
            }).ToAsyncEnumerable().ToListAsync();
            
            return filteredBooks;
        }

        public async Task<GetBookByIdDTO> GetBookById(int id)
        {
            Book requestedBook = await _bookRepository.GetBookById(id);
            if(requestedBook is null)
            {
                throw new EntityNotFoundException("Book with such ID couldn't be found.");
            }

            return new GetBookByIdDTO()
            {
                Description = requestedBook.Description,
                Title = requestedBook.Title,
                Cost=requestedBook.Cost
            };
           
        }
    }
}