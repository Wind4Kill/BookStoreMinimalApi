using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
using BookStoreMinimalApi.Domain.Interfaces.Services;
using BookStoreMinimalApi.Domain.FiltrationEntities;
using BookStoreMinimalApi.Domain.Entities;
using AutoMapper.QueryableExtensions;
using AutoMapper;
namespace BookStoreMinimalApi.Application
{
    public class BookService : IBookService
    {
        IAuthorRepository _authorRepository;
        readonly IBookRepository _bookRepository;
        readonly ICategoryRepository _categoryRepository;
        readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Book> CreateBook(CreateBookDto bookDto)
        {
            List<Category> bookCategories = new List<Category>();
            string[] categoriesNames = bookDto.Categories.Select(c => c.CategoryName).ToArray();

            Author? checkAuthor = await _authorRepository.GetAuthorByName(bookDto.Author.Name);
            List<Category>? checkCategories = await _categoryRepository.GetCategoriesByName(categoriesNames);
            if (checkCategories is not null)
            {
                string[]? absentCategoryNames = categoriesNames.Except(checkCategories?.Select(c => c.CategoryName).ToArray()!).ToArray();
                foreach (string category in absentCategoryNames)
                {
                    bookCategories.Add(new Category { CategoryName = category });
                }
                bookCategories.AddRange(checkCategories!);
            }
            else
            {
                foreach (string category in categoriesNames)
                {
                    bookCategories.Add(new Category { CategoryName = category });
                }
            }

            Book createdBook = new Book()
            {
                Title = bookDto.Title,
                Description = bookDto.Description,
                Cost = bookDto.Cost,
                Author = checkAuthor ?? new Author { Name = bookDto.Author.Name },
                Categories = bookCategories
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
            IQueryable<Book> filteredBooks = _bookRepository.GetAllBooks().
            OrderEntities(filters.OrderOptions, filters.FilterValue!).FilterEntities(filters.FilterOptions, filters.FilterValue!).
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
    }
}