using BookStoreMinimalApi.Application.Books.DTOs.BookDTOs;
using BookStoreMinimalApi.Application.Books.FiltrationEntities;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;

namespace BookStoreMinimalApi.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<List<GetBookDTO>> GetAllBooks(Filtration filters, CancellationToken cancellationToken);

        Task<GetBookByIdDTO> GetBookById(int id, CancellationToken cancellationToken);

        Task<GetBookByIdDTO> CreateBook(CreateBookDto bookDto, CancellationToken cancellationToken);

        Task DeleteBook(int id, CancellationToken cancellationToken);

        public Task UpdateBook(int id, ChangeBookDto changeBook, CancellationToken cancellationToken);
        
        
    }
}