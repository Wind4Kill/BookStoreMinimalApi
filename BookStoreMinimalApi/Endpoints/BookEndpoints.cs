using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.Interfaces.Services;

namespace BookStoreMinimalApi.Endpoints
{
    public static class BookEndpoints
    {
        public static void AddBookEndpoints(this WebApplication app)
        {
            var bookEndpoints = app.MapGroup("Book").WithTags("Books");
            bookEndpoints.MapGet("", async (IBookService service) =>
            {
                List<GetBookDTO>? books = await service.GetAllBooks();
                return books is not null ? Results.Ok(books)
                : Results.Problem("Entity wasn't found", statusCode: 404);
            }).Produces<List<GetBookDTO>>().ProducesProblem(404);

            app.MapGet("{id:int}", async (int id, IBookService service) =>
            {
                GetBookByIdDTO requestedBook = await service.GetBookById(id);
                return Results.Ok(requestedBook);
            }).Produces<GetBookByIdDTO>().ProducesProblem(statusCode:404);
        }

    }
}