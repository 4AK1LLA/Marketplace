namespace Marketplace.Core.DTO;

public class MainCategoryDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? PhotoUrl { get; set; }

    public IEnumerable<GetCategoryDto>? SubCategories { get; set; }
}