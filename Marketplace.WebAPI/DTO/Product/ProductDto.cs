namespace Marketplace.WebAPI.DTO;

public class ProductDto
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? PublicationDate { get; set; }

    public string? Location { get; set; }

    public string? MainPhotoUrl { get; set; }

    public string? PriceInfo { get; set; }

    public string? Condition { get; set; }
}