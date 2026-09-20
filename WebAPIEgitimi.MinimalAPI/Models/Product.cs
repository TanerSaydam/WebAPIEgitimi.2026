namespace WebAPIEgitimi.MinimalAPI.Models;

public sealed class Product
{
    public Product()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid CategoryId { get; set; }
}