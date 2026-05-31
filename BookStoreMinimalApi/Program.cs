using BookStoreMinimalApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddDbContext<ApplicationContext>(options =>
{
      if (builder.Environment.IsDevelopment())
      {
            string? connectionString = builder.Configuration.GetConnectionString("PostgreConnectionString");
            options.UseNpgsql(connectionString);
      }
});

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

app.MapGet("/", () => "Hello World!");

app.Run();
