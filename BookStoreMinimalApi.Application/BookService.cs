using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
using BookStoreMinimalApi.Domain.Interfaces.Services;
using BookStoreMinimalApi.Domain.FiltrationEntities;
using BookStoreMinimalApi.Domain.Entities;

using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using BookStoreMinimalApi.Api.Endpoints;
namespace BookStoreMinimalApi.Application
{
    public class BookService : IBookService
    {
        readonly IAuthorService _authorService;
        readonly IBookRepository _bookRepository;
        readonly ICategoryService _categoryService;
        readonly IMapper _mapper;
        readonly CustomMemoryCache _cache;

        public BookService(IBookRepository bookRepository,
        IAuthorService authorService,
        ICategoryService categoryService,
        IMapper mapper,
        CustomMemoryCache cache)
        {
            _bookRepository = bookRepository;
            _authorService = authorService;
            _categoryService = categoryService;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Book> CreateBook(CreateBookDto bookDto)
        {
            Author? checkAuthor = await _authorService.CheckExistingAuthor(bookDto.Author.Name);

            var bookCategories = await _categoryService.CheckExistingCategories(bookDto.Categories.Select(c => c.CategoryName).ToArray());

            Book createdBook = new Book()
            {
                Title = bookDto.Title,
                Description = bookDto.Description,
                Cost = bookDto.Cost,
                Author = checkAuthor ?? new Author(bookDto.Author.DateOfBirth) { Name = bookDto.Author.Name },
                Categories = bookCategories
            };

            return await _bookRepository.AddBook(createdBook);
        }

        public async Task<int> DeleteBook(int id)
        {
            Book requestedBook = await CheckIfBookExistsOrThrowException(id);
            int affectedRows = await _bookRepository.DeleteBook(requestedBook);
            string key = $"Book_{id}";
            _cache.Cache.Remove(key);
            return affectedRows;
        }

        public async Task<List<GetBookDTO>> GetAllBooks(Filtration filters)
        {
            IQueryable<Book> filteredBooks = _bookRepository.GetAllBooks().
            OrderEntities(filters.OrderOptions, filters.FilterValue!).
            FilterEntities(filters.FilterOptions, filters.FilterValue!).
            Paginate(filters.PageNum);

            List<GetBookDTO> mappedBooks = await _bookRepository.ToListAsync(
                _mapper.ProjectTo<GetBookDTO>(filteredBooks)
            );

            return mappedBooks;

        }

        public async Task<GetBookByIdDTO> GetBookById(int id)
        {
            string key = $"Book_{id}";
            GetBookByIdDTO? mappedBook = await _cache.Cache.GetOrCreateAsync(key, async (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                entry.Size = 1;
                Book requestedBook = await CheckIfBookExistsOrThrowException(id);
                return _mapper.Map<GetBookByIdDTO>(requestedBook);
            });

            return mappedBook!;
        }

        public async Task<int> UpdateBook(int id, ChangeBookDto changeBook)
        {
            Book requestedBook = await CheckIfBookExistsOrThrowException(id);

            _mapper.Map(changeBook, requestedBook);

            int result = await _bookRepository.UpdateBook();

            string key = $"Book_{id}";

            _cache.Cache.Remove(key);

            return result;
        }

        private async Task<Book> CheckIfBookExistsOrThrowException(int id)
        {
            Book? requestedBook = await _bookRepository.GetBookById(id);
            if (requestedBook is null)
            {
                throw new EntityNotFoundException("Book with such ID wasn't found.");
            }
            return requestedBook;
        }

    }
}