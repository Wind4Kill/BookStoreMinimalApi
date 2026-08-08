using System.Collections.Immutable;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.Entities;
using BookStoreMinimalApi.Domain.FiltrationEntities;
using BookStoreMinimalApi.Domain.Interfaces.Services;
using Microsoft.AspNetCore.OutputCaching;

namespace BookStoreMinimalApi.Endpoints
{
    public static class BookEndpoints
    {
        public static void AddBookEndpoints(this WebApplication app)
        {
            var bookEndpoints = app.MapGroup("api/books").WithTags("Books");
            
            bookEndpoints.MapGet("", async ([AsParameters] Filters filters, IBookService service, CancellationToken cancellationToken) =>
            {
                Filtration filtration = new(filterOptions: filters.FilterOptions, orderOptions: filters.OrderOptions,
                filterValue: filters.FilterValue, pageNum: filters.PageNum);
               
                List<GetBookDTO>? booksDtos = await service.GetAllBooks(filtration, cancellationToken);
                return Results.Ok(booksDtos);

            }).WithParameterValidation().Produces<List<GetBookDTO>>().
            CacheOutput(builder=>builder.Expire(TimeSpan.FromSeconds(120)).Tag("all-books"));

            bookEndpoints.MapGet("{id:int}", async (int id, IBookService service, CancellationToken cancellationToken) =>
            {
                GetBookByIdDTO requestedBookDto = await service.GetBookById(id, cancellationToken);
                return Results.Ok(requestedBookDto);
            }).CacheOutput().Produces<GetBookByIdDTO>().WithName("GetBookById");

            bookEndpoints.MapPost("", async (CreateBookDto bookDto, IBookService service, LinkGenerator linkGenerator,
            IOutputCacheStore cache, CancellationToken cancellationToken) =>
            {
                GetBookByIdDTO createdBook = await service.CreateBook(bookDto, cancellationToken);
                string? link = linkGenerator.GetPathByName(endpointName: "GetBookById", new { id = createdBook.BookId }, options: new LinkOptions() { LowercaseUrls = true });
                await cache.EvictByTagAsync("all-books", default);
                return Results.Created(link, createdBook);
            }).WithParameterValidation().Produces(201);

            bookEndpoints.MapPost("{id:int}/reviews", async (int id, IReviewService reviewService,
             ReviewDto reviewDto, LinkGenerator links, CancellationToken cancellationToken) =>
            {
                ReviewDto createdReview = await reviewService.AddReview(id, reviewDto, cancellationToken);
                string? link = $"{links.GetPathByName("GetBookById", new { id = id })}/reviews";
                
                return Results.Created(link, createdReview);
            });

            bookEndpoints.MapDelete("{id:int}", async (int id, IBookService service,
            IOutputCacheStore cache, CancellationToken cancellationToken) =>
            {
                await service.DeleteBook(id, cancellationToken);
                await cache.EvictByTagAsync("all-books", default);
                return Results.NoContent();
            });

            bookEndpoints.MapPut("{id:int}", async (int id, ChangeBookDto changeBookDto,
            IBookService service, IOutputCacheStore cache, CancellationToken cancellationToken) =>
            {
                await service.UpdateBook(id, changeBookDto, cancellationToken);
                await cache.EvictByTagAsync("all-books", default);
                return Results.NoContent();
            });

        }

    }
}