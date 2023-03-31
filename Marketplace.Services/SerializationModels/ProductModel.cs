namespace Marketplace.Services.SerializationModels;

internal class ProductModel
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? PublicationDate { get; set; }

    public string? Location { get; set; }

    public string? CategoryName { get; set; }

    public Dictionary<string, string>? TagValues { get; set; }

    public List<string>? PhotoUrls { get; set; }
}
