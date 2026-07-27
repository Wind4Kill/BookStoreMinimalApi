using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
using BookStoreMinimalApi.Domain.Interfaces.Services;
using BookStoreMinimalApi.Domain.FiltrationEntities;
using BookStoreMinimalApi.Domain.Entities;
using AutoMapper;
using BookStoreMinimalApi.Domain.Interfaces;
namespace BookStoreMinimalApi.Application
{
    public class BookService : IBookService
    {
        readonly IAuthorService _authorService;
        readonly IBookRepository _bookRepository;
        readonly ICategoryService _categoryService;
        readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, IAuthorService authorService, ICategoryService categoryService, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorService = authorService;
            _categoryService = categoryService;
            _mapper = mapper;
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
            Book? requestedBook = await _bookRepository.GetBookById(id);
            if (requestedBook is null)
            {
                throw new EntityNotFoundException("Book with such ID wasn't found and can't be deleted.");
            }
            int affectedRows = await _bookRepository.DeleteBook(id);
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
            Book? requestedBook = await _bookRepository.GetBookById(id);
            if (requestedBook is null)
            {
                throw new EntityNotFoundException("Book with such ID couldn't be found.");
            }

            GetBookByIdDTO mappedBook = _mapper.Map<GetBookByIdDTO>(requestedBook);

            return mappedBook;
        }

        public async Task<int> UpdateBook(int id, ChangeBookDto changeBook, IValidator<ChangeBookDto> validator)
        {
            Book? requestedBook = await _bookRepository.GetBookById(id);
            if (requestedBook is null)
            {
                throw new EntityNotFoundException("Book with such ID wasn't found.");
            }

            Dictionary<string, string> validatedValues = validator.Validate(changeBook);

            if (validatedValues.Count > 0)
            {
                Type changeBookType = typeof(Book);
                string[] propertyNames = changeBookType.GetProperties().Select(p => p.Name).ToArray();

                foreach (KeyValuePair<string, string> pair in validatedValues)
                {

                    if (propertyNames.Contains(pair.Key))
                    {
                        var requestedBookProperty = requestedBook.GetType().GetProperty(pair.Key);
                        requestedBookProperty!.SetValue(requestedBook, pair.Value);
                    }
                }
            }

            return await _bookRepository.UpdateBook();
        }

    }
}