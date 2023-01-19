using Marketplace.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data;

public class MarketplaceContext : DbContext
{
    public MarketplaceContext(DbContextOptions options) : base(options) {    }

    public DbSet<Product>? Products { get; set; }

    public DbSet<Category>? Categories { get; set; }

    public DbSet<Tag>? Tags { get; set; }

    public DbSet<Tag>? TagValues { get; set; }

    public DbSet<Photo>? Photos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Product>()
            .HasMany(pr => pr.TagValues)
            .WithOne(tv => tv.Product);

        builder
            .Entity<Product>()
            .HasOne(pr => pr.Category)
            .WithMany(ct => ct.Products);

        builder
            .Entity<Category>()
            .HasMany(ct => ct.Products)
            .WithOne(pr => pr.Category);

        builder
            .Entity<Category>()
            .HasMany(ct => ct.Tags)
            .WithMany(tg => tg.Categories);

        builder
            .Entity<Tag>()
            .HasMany(tg => tg.Categories)
            .WithMany(ct => ct.Tags);

        builder
            .Entity<TagValue>()
            .HasOne(tv => tv.Product)
            .WithMany(pr => pr.TagValues);
    }
}