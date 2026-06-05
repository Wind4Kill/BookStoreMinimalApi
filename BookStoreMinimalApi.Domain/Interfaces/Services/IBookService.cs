using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.FiltrationEntities;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;

namespace BookStoreMinimalApi.Domain.Interfaces.Services
{
    public interface IBookService
    {
        Task<List<GetBookDTO>> GetAllBooks(Filtration filters);

        Task<GetBookByIdDTO> GetBookById(int id);

        Task<Book> CreateBook(CreateBookDto bookDto);

        Task<int> DeleteBook(int id);
        
        
    }
}