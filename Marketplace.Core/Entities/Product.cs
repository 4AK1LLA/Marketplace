namespace Marketplace.Core.Entities;

public class Product
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime PublicationDate { get; set; }

    public string? Location { get; set; }

    public Category? Category { get; set; }

    public AppUser? AppUser { get; set; }

    public ICollection<TagValue>? TagValues { get; set; }

    public ICollection<Photo>? Photos { get; set; }

    public ICollection<AppUser> UsersThatLiked { get; set; } = new List<AppUser>();
}