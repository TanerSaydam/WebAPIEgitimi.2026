namespace WebAPIEgitimi.MinimalAPI.Models;

public sealed class Basket
{
    public Basket()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
}