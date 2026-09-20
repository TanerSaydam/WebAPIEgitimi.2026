using Carter;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebAPIEgitimi.MinimalAPI.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseInMemoryDatabase("MyDb");
});
builder.Services.AddCarter();
builder.Services.AddOpenApi();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapCarter();

app.Run();