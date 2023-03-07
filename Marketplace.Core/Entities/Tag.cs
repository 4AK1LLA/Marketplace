namespace Marketplace.Core.Entities;

public class Tag
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool IsRequired { get; set; }

    public string? Type { get; set; }

    public string? Remark { get; set; }

    public ICollection<PossibleValue>? PossibleValues { get; set; }

    public ICollection<Category>? Categories { get; set; }
}

// Aggregate type
public class PossibleValue
{
    public string? Value { get; set; }
}