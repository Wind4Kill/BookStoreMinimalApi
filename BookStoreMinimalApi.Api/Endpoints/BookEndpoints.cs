using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.FiltrationEntities;
using BookStoreMinimalApi.Domain.Interfaces.Services;

namespace BookStoreMinimalApi.Endpoints
{
    public static class BookEndpoints
    {
        public static void AddBookEndpoints(this WebApplication app)
        {
            var bookEndpoints = app.MapGroup("api/books").WithTags("Books");
            bookEndpoints.MapGet("", async ([AsParameters] Filters filters, IBookService service) =>
            {
                Filtration filtration = new();
                if (filters is not null)
                {
                    if (filters.FilterValue is not null)
                    {
                        filtration.FilterValue = filters.FilterValue;
                    }
                    if (filters.FilterOptions is not null)
                    {
                        filtration.FilterOptions = Enum.Parse<FilterOptions>(filters.FilterOptions);
                    }
                    if (filters.OrderOptions is not null)
                    {
                        filtration.OrderOptions = Enum.Parse<OrderOptions>(filters.OrderOptions);
                    }
                    if (filters.PageNum is not null)
                    {
                        filtration.PageNum = filters.PageNum.Value;
                    }
                }
                List<GetBookDTO>? booksDtos = await service.GetAllBooks(filtration);
                return Results.Ok(booksDtos);

            }).WithParameterValidation().Produces<List<GetBookDTO>>();

            bookEndpoints.MapGet("{id:int}", async (int id, IBookService service) =>
            {
                GetBookByIdDTO requestedBookDto = await service.GetBookById(id);
                return Results.Ok(requestedBookDto);
            }).Produces<GetBookByIdDTO>().WithName("GetBookById");

            bookEndpoints.MapPost("", async (CreateBookDto bookDto, IBookService service, LinkGenerator linkGenerator) =>
            {
                Book createdBook = await service.CreateBook(bookDto);
                string? link = linkGenerator.GetPathByName(endpointName: "GetBookById", new { id = createdBook.BookId }, options: new LinkOptions() { LowercaseUrls = true });
                return Results.Created(link, createdBook);
            }).WithParameterValidation().Produces(201);

            bookEndpoints.MapPost("{id:int}/reviews", async (int id, IReviewService reviewService, ReviewDto reviewDto) =>
            {
                await reviewService.AddReview(id, reviewDto);
                return Results.Ok();

            });

            bookEndpoints.MapDelete("{id:int}", async (int id, IBookService service) =>
            {
                await service.DeleteBook(id);
                return Results.NoContent();
            });

            bookEndpoints.MapPut("{id:int}", async (int id, ChangeBookDto changeBookDto, IBookService service) =>
            {
                await service.UpdateBook(id, changeBookDto);
                return Results.NoContent();
            });

        }

    }
}