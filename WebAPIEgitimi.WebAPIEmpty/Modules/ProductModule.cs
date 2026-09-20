using Carter;

namespace WebAPIEgitimi.WebAPIEmpty.Modules;

public class ProductModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products-minimal", () =>
        {
            string[] products = ["Bilgisayar", "Telefon", "Tablet"];

            return Results.Ok(products);
        });
    }
}