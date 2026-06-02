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
                List<GetBookDTO>? books = await service.GetAllBooks(filtration);
                return books is not null ? Results.Ok(books)
                : Results.Problem("Entity wasn't found", statusCode: 404);
            }).WithParameterValidation().Produces<List<GetBookDTO>>().ProducesProblem(404);

            bookEndpoints.MapGet("{id:int}", async (int id, IBookService service) =>
            {
                GetBookByIdDTO requestedBook = await service.GetBookById(id);
                return Results.Ok(requestedBook);
            }).Produces<GetBookByIdDTO>().ProducesProblem(statusCode: 404);
        }

    }
}