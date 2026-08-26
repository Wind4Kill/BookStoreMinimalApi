using BookStoreMinimalApi.Api.Endpoints;
using BookStoreMinimalApi.Application.Authors.Services;
using BookStoreMinimalApi.Application.Books.Services;
using BookStoreMinimalApi.Application.Categories.Services;
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Application.Reviews.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreMinimalApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddSingleton<CustomMemoryCache>();

            return services;
        }
    }
}