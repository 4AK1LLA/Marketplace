namespace Marketplace.Core.Entities;

public class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    public DateTime PublicationDate { get; set; }

    public string? Location { get; set; }

    public ICollection<Photo>? Photos { get; set; }

    public ICollection<Category>? Categories { get; set; }

    //TODO: add identity user property for seller
}