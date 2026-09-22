using System.Collections;
using System.Security.Claims;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMinimalApi
{
    public static class DBExtensions
    {
        public async static Task SeedData(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();

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
            await using var scope = app.Services.CreateAsyncScope();

            var _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            if (_context.Database.GetPendingMigrations().Any())
            {
                await _context.Database.MigrateAsync();
            }
        }

        public static async Task AddAdministrator(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();

            UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            User? admin = await userManager.FindByEmailAsync(app.Configuration["AdminCredentials:Email"]!);

            if (admin is null)
            {
                admin = new User(app.Configuration["AdminCredentials:Login"]!)
                {
                    Email = app.Configuration["AdminCredentials:Email"],
                };

                List<Claim> claims = new()
                {
                    new Claim("Role", "Admin")
                };

                await userManager.CreateAsync(admin, app.Configuration["AdminCredentials:Password"]!);
                await userManager.AddClaimsAsync(admin, claims);
            }
            return;

        }

    }
}