public static class Extensions
{
    public static void MapProducts(this IEndpointRouteBuilder app)
    {

        app.MapGet("products", () =>
        {
            string[] products = ["Bilgisayar", "Telefon", "Tablet"];

            return Results.Ok(products);
        });
    }
}