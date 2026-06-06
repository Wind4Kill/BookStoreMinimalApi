using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi
{
    public static class HelpClass
    {
        public async static void SeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();

            ApplicationContext _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            if (!_context.Books.Any())
            {
                List<Book> addedBooks = new List<Book>()
                {
                    new Book
                    {
                        Cost=1000,
                         Title="C# in details",
                        Description = "Book for C# pros",
                        Author=new Author()
                        {
                            Name= "Anders Helsberg"
                        }
                    },
                    new Book
                    {
                        Cost=1500,
                        Title="Java advanced",
                        Description = "Java for mastery",
                        Author = new Author()
                        {
                            Name= "James Gosling"
                        }
                    }
                };
                _context.AddRange(addedBooks);
                await _context.SaveChangesAsync();
            }
        }
    }
}