using MicroOrder.ProductService.API.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroOrder.ProductService.API.Infrastructure;

public class ProductDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
    }
}
