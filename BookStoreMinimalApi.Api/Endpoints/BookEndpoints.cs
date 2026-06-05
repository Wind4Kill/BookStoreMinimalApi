using BookStoreMinimalApi.Application;
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
            var bookEndpoints = app.MapGroup("Book").WithTags("Books");
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
            }).Produces<GetBookByIdDTO>().ProducesProblem(statusCode: 404).WithName("GetBookById");

            bookEndpoints.MapPost("", async (CreateBookDto bookDto, IBookService service, LinkGenerator linkGenerator) =>
            {
                Book createdBook = await service.CreateBook(bookDto);
                string? link = linkGenerator.GetPathByName("GetBookById", new LinkOptions() { LowercaseUrls = true });
                return Results.Created(link, createdBook);
            });

            bookEndpoints.MapDelete("/{id:int}", async (int id, IBookService service) =>
            {
                int affectedRows = await service.DeleteBook(id);
                return Results.NoContent();
            });

        }

    }
}