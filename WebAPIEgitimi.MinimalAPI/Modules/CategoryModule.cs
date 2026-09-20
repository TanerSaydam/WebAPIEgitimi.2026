using Carter;
using WebAPIEgitimi.MinimalAPI.Context;
using WebAPIEgitimi.MinimalAPI.Dto;
using WebAPIEgitimi.MinimalAPI.Models;

namespace WebAPIEgitimi.MinimalAPI.Modules;

public sealed class CategoryModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("categories").WithTags("Categories");

        app.MapGet(string.Empty, (ApplicationDbContext dbContext) =>
        {
            var res = dbContext.Categories.ToList();

            return res;
        });

        app.MapPost(string.Empty, (CategoryCreateDto request, ApplicationDbContext dbContext) =>
        {
            Category category = new()
            {
                Name = request.Name
            };

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            return new { Id = category.Id, Message = "Kayıt işlemi başarıyla tamamlandı" };
        });
    }
}