namespace Marketplace.WebAPI.DTO;

public class MainCategoryDto
{
    public string? Route { get; set; }

    public string? Name { get; set; }

    public string? PhotoUrl { get; set; }

    public ICollection<GetCategoryDto>? SubCategories { get; set; }
}