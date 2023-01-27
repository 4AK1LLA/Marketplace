namespace Marketplace.WebAPI.DTO;

public class ProductDto
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateTime PublicationDate { get; set; }

    public string? Location { get; set; }

    public ICollection<TagValueDto>? TagValues { get; set; }

    public PhotoDto? MainPhoto { get; set; }
}