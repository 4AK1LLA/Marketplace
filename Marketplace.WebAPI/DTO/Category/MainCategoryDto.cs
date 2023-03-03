namespace Marketplace.WebAPI.DTO;

public class MainCategoryDto
{
    public string? Route { get; set; }

    public string? Name { get; set; }

    public string? PhotoUrl { get; set; }

    public ICollection<CategoryDto>? SubCategories { get; set; }
}