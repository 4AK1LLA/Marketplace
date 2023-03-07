namespace Marketplace.WebAPI.DTO;

public class MainCategoryPostDto
{
    public string? Name { get; set; }

    public string? PhotoUrl { get; set; }

    public ICollection<CategoryPostDto>? SubCategories { get; set; }
}
