﻿using Marketplace.Core.Entities;
using Marketplace.Data.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Marketplace.Data;

public class MarketplaceContext : DbContext
{
    private readonly IOptions<AppDbOptions> _options;

    public MarketplaceContext(IOptions<AppDbOptions> options) { _options = options; }

    public DbSet<Product>? Products { get; set; }

    public DbSet<Category>? Categories { get; set; }

    public DbSet<Tag>? Tags { get; set; }

    public DbSet<Tag>? TagValues { get; set; }

    public DbSet<Photo>? Photos { get; set; }

    public DbSet<MainCategory>? MainCategories { get; set; }

    public DbSet<AppUser>? AppUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (_options.Value.UseInMemoryDb)
        {
            options.UseInMemoryDatabase(_options.Value.InMemoryDbName!);
        }
        else
        {
            options.UseSqlServer(_options.Value.ConnectionStrings!["DefaultConnection"]);
        }
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

        builder
            .Entity<AppUser>()
            .HasMany(us => us.Products)
            .WithOne(pr => pr.AppUser);

        builder
            .Entity<Product>()
            .HasOne(pr => pr.AppUser)
            .WithMany(us => us.Products);

        builder.Entity<Tag>().OwnsMany(
            tg => tg.PossibleValues, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            });

        builder
            .Entity<AppUser>()
            .HasMany(us => us.LikedProducts)
            .WithMany(pr => pr.UsersThatLiked);

        builder
            .Entity<Product>()
            .HasMany(pr => pr.UsersThatLiked)
            .WithMany(us => us.LikedProducts);
    }
}