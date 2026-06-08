using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
using BookStoreMinimalApi.Domain.Interfaces.Services;
using BookStoreMinimalApi.Domain.FiltrationEntities;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Application
{
    public class BookService : IBookService
    {
        readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> CreateBook(CreateBookDto bookDto)
        {
            Book createdBook = new Book()
            {
                Title = bookDto.Title,
                Description = bookDto.Description,
                Cost = bookDto.Cost,
                Author = new Author { Name = bookDto.Author.Name }
            };

            return await _bookRepository.AddBook(createdBook);
        }

        public async Task<int> DeleteBook(int id)
        {
            try
            {
                int affectedRows = await _bookRepository.DeleteBook(id);
                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new EntityNotFoundException("Book with such ID wasn't found and can't be deleted.", ex);
            }
        }

        public async Task<List<GetBookDTO>> GetAllBooks(Filtration filters)
        {
            List<GetBookDTO> filteredBooks = await _bookRepository.ToListAsync(_bookRepository.GetAllBooks().
            OrderEntities(filters.OrderOptions, filters.FilterValue!).FilterEntities(filters.FilterOptions, filters.FilterValue!).
            Paginate(filters.PageNum).Select(b =>
            new GetBookDTO
            {
                BookId = b.BookId,
                Title = b.Title,
                Cost = b.Cost
            }));

            return filteredBooks;
        }

        public async Task<GetBookByIdDTO> GetBookById(int id)
        {
            Book? requestedBook = await _bookRepository.GetBookById(id);
            if (requestedBook is null)
            {
                throw new EntityNotFoundException("Book with such ID couldn't be found.");
            }

            return new GetBookByIdDTO()
            {
                Description = requestedBook.Description,
                Title = requestedBook.Title,
                Cost = requestedBook.Cost,
                AuthorName = requestedBook.Author.Name
            };

        }
    }
}