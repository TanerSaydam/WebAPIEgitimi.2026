using Microsoft.EntityFrameworkCore;
using WebAPIEgitimi.MinimalAPI.Models;

namespace WebAPIEgitimi.MinimalAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Basket> Baskets { get; set; }
}