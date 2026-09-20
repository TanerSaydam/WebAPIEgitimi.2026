using Carter;
using Microsoft.EntityFrameworkCore;
using WebAPIEgitimi.MinimalAPI.Context;
using WebAPIEgitimi.MinimalAPI.Dto;
using WebAPIEgitimi.MinimalAPI.Models;

namespace WebAPIEgitimi.MinimalAPI.Modules;

public sealed class ProductModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("products").WithTags("Products");

        app.MapGet(string.Empty, async (ApplicationDbContext dbContext, CancellationToken cancellationToken) =>
        {
            var res = await dbContext.Products
            .LeftJoin(dbContext.Categories, m => m.CategoryId, m => m.Id, (product, category) => new { product, category })
            .Select(s => new ProductDto(s.product.Id, s.product.Name, s.product.CategoryId, s.category!.Name))
            .ToListAsync(cancellationToken);

            return res;
        });

        app.MapPost(string.Empty, async (ProductCreateDto request, ApplicationDbContext dbContext, CancellationToken cancellationToken) =>
        {
            Product product = new()
            {
                Name = request.Name,
                CategoryId = request.CategoryId
            };

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new { Message = "Kayıt işlemi başarıyla tamamlandı" };
        });
    }
}