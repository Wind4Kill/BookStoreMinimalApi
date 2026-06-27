
using BookStoreMinimalApi.Application;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Data.Repositories;
using BookStoreMinimalApi.Domain.Entities;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
using BookStoreMinimalApi.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMinimalApi
{
    public static class HelpClass
    {
        public async static Task SeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();

            ApplicationContext _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            if (!_context.Books.Any())
            {
                Category category = new Category { CategoryName = "Programming" };
                List<Book> addedBooks = new List<Book>()
                {
                    new Book
                    {
                        Categories = new List<Category>() {category},
                        Cost=1000,
                         Title="C# in details",
                        Description = "Book for C# pros",
                        Author=new Author(new DateTime(1960, 10, 15))
                        {
                            Name= "Anders Helsberg"
                        }
                    },
                    new Book
                    {
                        Cost=1500,
                        Title="Java advanced",
                        Description = "Java for mastery",
                        Author = new Author(new DateTime(1980, 11, 19))
                        {
                            Name= "James Gosling"
                        },
                        Categories = new List<Category>(){category}

                    }
                };
                _context.AddRange(addedBooks);
                await _context.SaveChangesAsync();
            }
            return;
        }

        public static async Task UpdateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();

            var _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            if (_context.Database.GetPendingMigrations().Any())
            {
                try
                {
                    await _context.Database.MigrateAsync();
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            return services;
        }
    }
}