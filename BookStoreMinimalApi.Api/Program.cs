using System.Diagnostics;
using System.Reflection;
using BookStoreMinimalApi;
using BookStoreMinimalApi.Application;
using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Data.Repositories;
using BookStoreMinimalApi.Domain.DTOs;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
using BookStoreMinimalApi.Domain.Interfaces.Services;
using BookStoreMinimalApi.Endpoints;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddAutoMapper(cfg =>
{
      cfg.AddMaps(Assembly.Load("BookStoreMinimalApi.Domain"));
});
builder.Services.AddDbContext<ApplicationContext>(options =>
{
      string? connectionString = builder.Configuration.GetConnectionString("PostgreConnectionString");
      options.UseNpgsql(connectionString, (options) => options.EnableRetryOnFailure(5, TimeSpan.FromMilliseconds(3000), null));

      if (builder.Environment.IsDevelopment())
      {
            options.LogTo((message) => Debug.WriteLine(message), LogLevel.Information).
            EnableSensitiveDataLogging().EnableDetailedErrors();
      }
});

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();


if (builder.Environment.IsDevelopment())
{

      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();
}

var app = builder.Build();

app.UseStatusCodePages();
if (app.Environment.IsProduction())
{
      app.UseExceptionHandler(errorApp =>
      {
            errorApp.Run(async context =>
            {
                  var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                  if (error is null)
                        return;

                  var (statusCode, message) = error switch

                  {
                        EntityNotFoundException => (StatusCodes.Status404NotFound, error.Message),
                        _ => (StatusCodes.Status500InternalServerError, error.Message)
                  };

                  var problemDetails = new ProblemDetails
                  {
                        Status = statusCode,
                        Title = message,
                        Type = $"https://httpstatuses.com/{statusCode}",
                        Detail = error.Message,
                        Instance = context.Request.Path
                  };

                  context.Response.StatusCode = statusCode;

                  await context.Response.WriteAsJsonAsync(problemDetails);
            });
      });

      await app.UpdateDatabase();

}

if (app.Environment.IsDevelopment())
{
      await app.SeedData();
      app.UseSwagger();
      app.UseSwaggerUI();
}

app.AddBookEndpoints();

app.Run();
