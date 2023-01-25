namespace Marketplace.WebAPI.DTO;

public class CreateProductDto
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public int CategoryId { get; set; }

    public Dictionary<int, string>? TagIdsAndValues { get; set; }
}