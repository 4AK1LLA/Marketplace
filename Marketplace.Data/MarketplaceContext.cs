using Marketplace.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data;

public class MarketplaceContext : DbContext
{
    public MarketplaceContext(DbContextOptions options) : base(options) {    }

    public DbSet<Product>? Products { get; set; }

    public DbSet<Category>? Categories { get; set; }

    public DbSet<Photo>? Photos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Product>()
            .HasMany(pr => pr.Categories)
            .WithMany(ct => ct.Products);

        builder
            .Entity<Product>()
            .HasMany(pr => pr.Photos)
            .WithOne(ph => ph.Product);
    }
}

