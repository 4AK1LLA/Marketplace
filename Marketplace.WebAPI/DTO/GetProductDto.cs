namespace Marketplace.WebAPI.DTO;

public class GetProductDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    public DateTime PublicationDate { get; set; }

    public string? Location { get; set; }

    public ICollection<string>? Categories { get; set; }
}