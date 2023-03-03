namespace Marketplace.WebAPI.DTO;

public class ProductDetailsDto
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? PublicationDate { get; set; }

    public string? Location { get; set; }

    public ICollection<TagValueDto>? TagValues { get; set; }

    public ICollection<PhotoDto>? Photos { get; set; }
}
