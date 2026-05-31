var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
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

if(app.Environment.IsDevelopment())
{
      app.UseSwagger();
      app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.Run();
