using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.Entities;
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
                List<Book> addedBooks = new List<Book>();
                Book book1 = new Book
                {
                    Cost = 1000,
                    Title = "C# in details",
                    Description = "Book for C# pros"
                };

                Author author1 = new Author()
                {
                    Name = "Anders Helsberg",
                };
                author1.SetBirthDate(DateTime.Parse("16.01.2004"));
                book1.Author = author1;

                Book book2 = new Book
                {
                    Cost = 1500,
                    Title = "Java advanced",
                    Description = "Java for mastery"
                };
                Author author2 = new Author()
                {
                    Name = "James Gosling"
                };
                author2.SetBirthDate(DateTime.Parse("17.01.2004"));
                book2.Author = author2;
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
    }
}