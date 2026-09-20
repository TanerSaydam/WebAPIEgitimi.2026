namespace WebAPIEgitimi.MinimalAPI.Models;

public sealed class Category
{
    public Category()
    {
        Id = Guid.CreateVersion7(); //güncel sıralanabilir versiyonu
        //Id = Guid.NewGuid(); //eski versiyonu
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}