namespace Marketplace.Core.Entities;

public class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public ICollection<Product>? Products { get; set; }

    public ICollection<Tag>? Tags { get; set; }

    public MainCategory? MainCategory { get; set; }
}