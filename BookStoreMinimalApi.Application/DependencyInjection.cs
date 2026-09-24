using BookStoreMinimalApi.Api.Endpoints;
using BookStoreMinimalApi.Application.Authorization;
using BookStoreMinimalApi.Application.Authors.Services;
using BookStoreMinimalApi.Application.Books.Services;
using BookStoreMinimalApi.Application.Categories.Services;
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Application.Reviews.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreMinimalApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddSingleton<CustomMemoryCache>();
            services.Configure<JwtTokenSettings>(configuration.GetSection("Jwt"));

            return services;
        }
    }
}