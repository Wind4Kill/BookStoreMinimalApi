using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using Microsoft.Extensions.Caching.Memory;
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Application.Interfaces.Repositories;
using AutoMapper;
using BookStoreMinimalApi.Application.Books.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.Entities;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Application.Books.FiltrationEntities;
using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Application.Interfaces.Abstractions;
using BookStoreMinimalApi.Api.Endpoints;
namespace BookStoreMinimalApi.Application.Books.Services
{
    public class BookService : IBookService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IAuthorService _authorService;
        readonly IBookRepository _bookRepository;
        readonly ICategoryService _categoryService;
        readonly IMapper _mapper;
        readonly ICacheService<Book> _cache;
        public BookService(IUnitOfWork unitOfWork,
        IBookRepository bookRepository,
        IAuthorService authorService,
        ICategoryService categoryService,
        IMapper mapper,
        ICacheService<Book> cache)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _authorService = authorService;
            _categoryService = categoryService;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<GetBookByIdDTO> CreateBook(CreateBookDto bookDto, CancellationToken cancellationToken)
        {
            Author? checkAuthor = await _authorService.CheckExistingAuthor(bookDto.Author.Name, cancellationToken);

            var bookCategories = await _categoryService.
            CheckExistingCategories(bookDto.Categories.Select(c => c.CategoryName).ToArray(),
            cancellationToken);

            Book createdBook = new Book()
            {
                Title = bookDto.Title,
                Description = bookDto.Description,
                Cost = bookDto.Cost,
                Author = checkAuthor ?? new Author(bookDto.Author.DateOfBirth) { Name = bookDto.Author.Name },
                Categories = bookCategories
            };

            createdBook = await _bookRepository.AddBook(createdBook);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            GetBookByIdDTO mappedBook = _mapper.Map<GetBookByIdDTO>(createdBook);
            return mappedBook;
        }

        public async Task DeleteBook(int id, CancellationToken cancellationToken)
        {
            Book requestedBook = await CheckIfBookExistsOrThrowException(id, cancellationToken);
            await _bookRepository.DeleteBook(requestedBook);
            await _unitOfWork.SaveChangesAsync();
            _cache.RemoveFromCache(id);
        }

        public async Task<List<GetBookDTO>> GetAllBooks(Filtration filters, CancellationToken cancellationToken)
        {
            List<Book> requestedBooks = await _bookRepository.GetAllBooks(filters, cancellationToken);

            List<GetBookDTO> mappedBooks = _mapper.Map<List<GetBookDTO>>(requestedBooks).ToList();

            return mappedBooks;
        }

        public async Task<GetBookByIdDTO> GetBookById(int id, CancellationToken cancellationToken)
        {
            Book? requestedBook = _cache.GetFromCache(id, cancellationToken);
            if (requestedBook is null)
            {
                requestedBook = await CheckIfBookExistsOrThrowException(id, cancellationToken);
                _cache.AddToCache(requestedBook, requestedBook.BookId, cancellationToken);
            }
            GetBookByIdDTO mappedBook = _mapper.Map<GetBookByIdDTO>(requestedBook);
            return mappedBook!;
        }

        public async Task UpdateBook(int id, ChangeBookDto changeBook, CancellationToken cancellationToken)
        {
            Book requestedBook = await CheckIfBookExistsOrThrowException(id, cancellationToken);

            _mapper.Map(changeBook, requestedBook);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _cache.RemoveFromCache(requestedBook.BookId);

        }

        private async Task<Book> CheckIfBookExistsOrThrowException(int id, CancellationToken cancellationToken)
        {
            Book? requestedBook = await _bookRepository.GetBookById(id, cancellationToken);
            if (requestedBook is null)
            {
                throw new EntityNotFoundException("Book with such ID wasn't found.");
            }
            return requestedBook;
        }

    }
}