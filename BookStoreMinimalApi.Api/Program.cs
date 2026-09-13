using System.Diagnostics;
using System.Reflection;
using BookStoreMinimalApi;
using BookStoreMinimalApi.Api;
using BookStoreMinimalApi.Api.Endpoints;
using BookStoreMinimalApi.Application;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Endpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
builder.Services.AddProblemDetails();
builder.Services.AddAuthentication().AddBearerToken();
builder.Services.AddAuthorization();
if (builder.Environment.IsProduction())
{
      builder.Services.AddStackExchangeRedisOutputCache((options) =>
      {
            options.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
            options.InstanceName = "bookstore-api-cache";

      });
}
builder.Services.AddOutputCache();
builder.Services.AddAutoMapper(cfg =>
{
      cfg.AddMaps(Assembly.Load("BookStoreMinimalApi.Domain"));
});

builder.Services.AddApplication();
builder.Services.AddData(builder.Configuration.GetConnectionString("PostgreConnectionString"));


if (builder.Environment.IsDevelopment() || builder.Environment.IsProduction())
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
app.UseRouting();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
      app.UseSwagger();
      app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseOutputCache();

app.AddBookEndpoints();
app.AddUserEndpoints();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
      await app.SeedData();
}
app.Run();
