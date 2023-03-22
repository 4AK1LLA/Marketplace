namespace Marketplace.Services.SerializationModels;

internal class TagModel
{
    public string? Identifier { get; set; }

    public string? Name { get; set; }

    public bool IsRequired { get; set; }

    public string? Type { get; set; }

    public string? Remark { get; set; }

    public List<string>? PossibleValues { get; set; }
}
