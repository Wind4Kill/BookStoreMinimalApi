using System.Diagnostics;
using System.Reflection;
using System.Text;
using BookStoreMinimalApi;
using BookStoreMinimalApi.Api;
using BookStoreMinimalApi.Api.Endpoints;
using BookStoreMinimalApi.Application;
using BookStoreMinimalApi.Data;
using BookStoreMinimalApi.Endpoints;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
      options.TokenValidationParameters = new TokenValidationParameters
      {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)),
            ClockSkew = TimeSpan.Zero
      };
});
builder.Services.AddAuthorization(policy =>
{
      policy.AddPolicy("IsAdmin", opts => opts.RequireClaim("Role",["Admin"]).RequireClaim("nickname", ["Admin"]));
});
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
      cfg.AddMaps(Assembly.Load("BookStoreMinimalApi.Application"));
});

builder.Services.AddApplication(builder.Configuration);
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
      //Remove in prod
      await app.AddAdministrator();
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
