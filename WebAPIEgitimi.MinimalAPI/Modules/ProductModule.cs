using Carter;
using WebAPIEgitimi.MinimalAPI.Context;
using WebAPIEgitimi.MinimalAPI.Dto;
using WebAPIEgitimi.MinimalAPI.Models;

namespace WebAPIEgitimi.MinimalAPI.Modules;

public sealed class ProductModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("products").WithTags("Products");

        app.MapGet(string.Empty, (ApplicationDbContext dbContext) =>
        {
            var res = dbContext.Products
            .LeftJoin(dbContext.Categories, m => m.CategoryId, m => m.Id, (product, category) => new { product, category })
            .Select(s => new ProductDto(s.product.Id, s.product.Name, s.product.CategoryId, s.category!.Name))
            .ToList();

            return res;
        });

        app.MapPost(string.Empty, (ProductCreateDto request, ApplicationDbContext dbContext) =>
        {
            Product product = new()
            {
                Name = request.Name,
                CategoryId = request.CategoryId
            };

            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            return new { Message = "Kayıt işlemi başarıyla tamamlandı" };
        });
    }
}