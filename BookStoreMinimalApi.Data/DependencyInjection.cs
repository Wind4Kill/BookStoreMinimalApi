using System.Collections.Immutable;

using System.Diagnostics;
using BookStoreMinimalApi.Application.Interfaces.Repositories;
using BookStoreMinimalApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Data.Services;
using BookStoreMinimalApi.Application.Interfaces.Abstractions.Authorization;
using BookStoreMinimalApi.Domain.Entities;
using BookStoreMinimalApi.Application.Interfaces.Abstractions;
using BookStoreMinimalApi.Data.Persistency;

namespace BookStoreMinimalApi.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services, string? connectionString)
        {
            if (connectionString is null)
            {
                throw new Exception("Connection string is not provided");
            }
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(connectionString, options => options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null))
                .LogTo((message) => Debug.WriteLine(message)).EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            });
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 8; ;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationContext>();

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenProvider, JwtTokenProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            return services;
        }
    }
}