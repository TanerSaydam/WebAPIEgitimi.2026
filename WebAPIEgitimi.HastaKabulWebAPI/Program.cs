using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebAPIEgitimi.HastaKabulWebAPI.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    string con = builder.Configuration.GetConnectionString("SqlServer")!;
    opt.UseSqlServer(con);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(x => x
  .AllowAnyOrigin()
  .AllowAnyHeader()
  .AllowAnyMethod()
  .SetPreflightMaxAge(TimeSpan.FromMinutes(10))
);

app.Run();