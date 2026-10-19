using Bank_API;
using Bank_API.ExceptionHandler;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddPresentDI(builder.Configuration);

var app = builder.Build();
app.UseExceptionHandler();
app.MapSwagger();
app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.MapGet("/api/users/{Id:int}", (int Id) =>
{
    var users = new[]
    {
        new { Id = 1, Name = "Atul" },
        new { Id = 2, Name = "Rahul" }
    };

    var result = users.FirstOrDefault(x => x.Id == Id);

    if (result == null)
        return Results.NotFound(
            $"User with ID {Id} not found.");

    return Results.Ok(result);
});

app.Run();