using System.Diagnostics;
using System.Reflection;
using BookStoreMinimalApi;
using BookStoreMinimalApi.Api;
using BookStoreMinimalApi.Application.Exceptions;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Endpoints;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddStackExchangeRedisOutputCache((options)=>
{
      options.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
      options.InstanceName = "bookstore-api-cache";

});
builder.Services.AddOutputCache();
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

builder.Services.AddServices();


if (builder.Environment.IsDevelopment()||builder.Environment.IsProduction())
{
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();
}

var app = builder.Build();

if (app.Environment.IsProduction())
{
      app.UseExceptionHandler();
      await app.UpdateDatabase();
}

app.UseStatusCodePages();
if (app.Environment.IsDevelopment()||app.Environment.IsProduction())
{
      app.UseSwagger();
      app.UseSwaggerUI();
}

app.UseOutputCache();
app.AddBookEndpoints();

if(app.Environment.IsDevelopment()||app.Environment.IsProduction())
{
      await app.SeedData();
}
app.Run();
