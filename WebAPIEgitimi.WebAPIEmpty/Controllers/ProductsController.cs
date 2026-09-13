using Microsoft.AspNetCore.Mvc;

namespace WebAPIEgitimi.WebAPIEmpty.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    [HttpGet] //Query params
    //[HttpGet("{name}")] //route params
    public IActionResult GetAll(string? name)
    {
        var httpContext = httpContextAccessor.HttpContext;
        var res = Product.Products;
        return Ok(res);
    }

    [HttpPost]
    public IActionResult Create(ProductDto request)
    {
        Product product = new()
        {
            Name = request.Name
        };

        Product.Products.Add(product);

        return NoContent();
    }

    public IActionResult Test2()
    {
        return Ok();
    }
}

public class Product
{
    public static List<Product> Products = new();
    public string Name { get; set; }
}

public record ProductDto(string Name);
public class ProductDto2
{
    public string Name { get; set; }
}