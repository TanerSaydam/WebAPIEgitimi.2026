using Carter;

namespace WebAPIEgitimi.WebAPIEmpty.Modules;

public class CategoryModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("categories", () =>
        {
            string[] categories = ["Bilgisayar", "Telefon", "Tablet"];

            return Results.Ok(categories);
        });
    }
}