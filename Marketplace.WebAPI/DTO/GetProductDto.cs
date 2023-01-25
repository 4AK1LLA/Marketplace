namespace Marketplace.WebAPI.DTO;

public class GetProductDto
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime PublicationDate { get; set; }

    public string? Location { get; set; }

    public string? Category { get; set; }

    public Dictionary<string, string>? TagNamesAndValues { get; set; }
}