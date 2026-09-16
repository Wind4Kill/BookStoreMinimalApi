using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using Microsoft.Extensions.Caching.Memory;
using BookStoreMinimalApi.Api.Endpoints;
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Application.Interfaces.Repositories;
using AutoMapper;
using BookStoreMinimalApi.Application.Books.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.Entities;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Application.Books.FiltrationEntities;
using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Application.Interfaces.Abstractions;
namespace BookStoreMinimalApi.Application.Books.Services
{
    public class BookService : IBookService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IAuthorService _authorService;
        readonly IBookRepository _bookRepository;
        readonly ICategoryService _categoryService;
        readonly IMapper _mapper;
        readonly CustomMemoryCache _cache;

        public BookService(IUnitOfWork unitOfWork,
        IBookRepository bookRepository,
        IAuthorService authorService,
        ICategoryService categoryService,
        IMapper mapper,
        CustomMemoryCache cache)
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

            createdBook = await _bookRepository.AddBook(createdBook, cancellationToken);

            GetBookByIdDTO mappedBook = _mapper.Map<GetBookByIdDTO>(createdBook);
            return mappedBook;
        }

        public async Task DeleteBook(int id, CancellationToken cancellationToken)
        {
            Book requestedBook = await CheckIfBookExistsOrThrowException(id, cancellationToken);
            await _bookRepository.DeleteBook(requestedBook, cancellationToken);
            string key = GetKeyById(id);
            _cache.Cache.Remove(key);
        }

        public async Task<List<GetBookDTO>> GetAllBooks(Filtration filters, CancellationToken cancellationToken)
        {
            List<Book> requestedBooks = await _bookRepository.GetAllBooks(filters, cancellationToken);

            List<GetBookDTO> mappedBooks = _mapper.Map<List<GetBookDTO>>(requestedBooks).ToList();

            return mappedBooks;
        }

        public async Task<GetBookByIdDTO> GetBookById(int id, CancellationToken cancellationToken)
        {
            string key = GetKeyById(id);
            GetBookByIdDTO? mappedBook = await _cache.Cache.GetOrCreateAsync(key, async (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                entry.Size = 1;
                Book requestedBook = await CheckIfBookExistsOrThrowException(id, cancellationToken);
                return _mapper.Map<GetBookByIdDTO>(requestedBook);
            });

            return mappedBook!;
        }

        public async Task UpdateBook(int id, ChangeBookDto changeBook, CancellationToken cancellationToken)
        {
            Book requestedBook = await CheckIfBookExistsOrThrowException(id, cancellationToken);

            _mapper.Map(changeBook, requestedBook);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            string key = GetKeyById(id);

            _cache.Cache.Remove(key);

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
        private string GetKeyById(int id) => $"Book:{id}";

    }
}