namespace Marketplace.Core.Entities;

public class Tag
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public ICollection<Category>? Categories { get; set; }
}