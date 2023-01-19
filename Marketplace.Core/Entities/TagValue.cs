namespace Marketplace.Core.Entities;

public class TagValue
{
    public int Id { get; set; }

    public string? Value { get; set; }

    public Tag? Tag { get; set; }
}