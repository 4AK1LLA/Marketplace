namespace Marketplace.Core.Entities;

public class MainCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? PhotoUrl { get; set; }

    public ICollection<Category>? SubCategories { get; set; }
}