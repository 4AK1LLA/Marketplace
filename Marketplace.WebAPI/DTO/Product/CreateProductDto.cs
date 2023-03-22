namespace Marketplace.WebAPI.DTO;

public class CreateProductDto
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public int CategoryId { get; set; }

    public IDictionary<int, string>? TagValuesDictionary { get; set; }
}
