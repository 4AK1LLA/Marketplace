namespace Marketplace.WebAPI.DTO;

public class CreateProductDto
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public int CategoryId { get; set; }

    public IEnumerable<TagValuePostDto>? TagValues { get; set; }
}
