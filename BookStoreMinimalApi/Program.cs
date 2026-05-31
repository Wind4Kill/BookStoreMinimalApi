using BookStoreMinimalApi;
using BookStoreMinimalApi.Application;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Data.Repositories;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
using BookStoreMinimalApi.Domain.Interfaces.Services;
using BookStoreMinimalApi.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddDbContext<ApplicationContext>(options =>
{
      string? connectionString = builder.Configuration.GetConnectionString("PostgreConnectionString");
      options.UseNpgsql(connectionString);

      if (builder.Environment.IsDevelopment())
      {
            options.LogTo(Console.WriteLine).EnableSensitiveDataLogging();
      }
});

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

if (builder.Environment.IsDevelopment())
{

      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();
}

var app = builder.Build();

if (app.Environment.IsProduction())
{
      app.UseExceptionHandler();
}

if (app.Environment.IsDevelopment())
{
      app.UseSwagger();
      app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
      app.SeedData();
}

app.AddBookEndpoints();

app.Run();
