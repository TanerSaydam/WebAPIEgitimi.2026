using Carter;
using Microsoft.EntityFrameworkCore;
using WebAPIEgitimi.MinimalAPI.Context;
using WebAPIEgitimi.MinimalAPI.Dto;
using WebAPIEgitimi.MinimalAPI.Models;

namespace WebAPIEgitimi.MinimalAPI.Modules;

public sealed class CategoryModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("categories").WithTags("Categories");

        app.MapGet(string.Empty, async (ApplicationDbContext dbContext, CancellationToken cancellationToken) =>
        {
            var res = await dbContext.Categories.ToListAsync(cancellationToken);

            return res;
        });

        app.MapPost(string.Empty, async (CategoryCreateDto request, ApplicationDbContext dbContext, CancellationToken cancellationToken) =>
        {
            Category category = new()
            {
                Name = request.Name
            };
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new { Id = category.Id, Message = "Kayıt işlemi başarıyla tamamlandı" };
        });
    }
}