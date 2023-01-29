using Marketplace.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Marketplace.Data;

public class MarketplaceContext : DbContext
{
    private readonly IConfiguration _configuration;

    public MarketplaceContext(IConfiguration configuration) { _configuration = configuration; }

    public DbSet<Product>? Products { get; set; }

    public DbSet<Category>? Categories { get; set; }

    public DbSet<Tag>? Tags { get; set; }

    public DbSet<Tag>? TagValues { get; set; }

    public DbSet<Photo>? Photos { get; set; }

    public DbSet<MainCategory>? MainCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")!);
    }

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

        builder
            .Entity<Category>()
            .HasOne(ct => ct.MainCategory)
            .WithMany(mc => mc.SubCategories);

        builder
            .Entity<MainCategory>()
            .HasMany(mc => mc.SubCategories)
            .WithOne(ct => ct.MainCategory);
    }
}