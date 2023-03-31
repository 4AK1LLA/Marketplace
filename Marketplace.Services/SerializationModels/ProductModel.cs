namespace Marketplace.Services.SerializationModels;

internal class ProductModel
{
    internal string? Title { get; set; }

    internal string? Description { get; set; }

    internal DateTime? PublicationDate { get; set; }

    internal string? Location { get; set; }

    internal string? CategoryName { get; set; }

    internal Dictionary<string, string>? TagValues { get; set; }

    internal List<string>? PhotoUrls { get; set; }
}
