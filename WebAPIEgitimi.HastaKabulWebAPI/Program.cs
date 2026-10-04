using Carter;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;
using WebAPIEgitimi.HastaKabulWebAPI.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    string con = builder.Configuration.GetConnectionString("SqlServer")!;
    opt.UseSqlServer(con);
});
builder.Services.AddCarter();
builder.Services.AddResponseCompression(x => x.EnableForHttps = true);
builder.Services.AddRateLimiter(x =>
{
    x.AddFixedWindowLimiter("fixed", o =>
    {
        o.PermitLimit = 100;
        o.QueueLimit = 100;
        o.Window = TimeSpan.FromSeconds(1);
        o.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
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

app.UseResponseCompression();
app.UseRateLimiter();

app.MapCarter();

app.Run();