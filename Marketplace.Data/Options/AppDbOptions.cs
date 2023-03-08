namespace Marketplace.Data.Options;

public class AppDbOptions
{
    public const string Position = "DbOptions";

    public bool UseInMemoryDb { get; set; }

    public string? InMemoryDbName { get; set; }

    public IDictionary<string, string>? ConnectionStrings { get; set; }
}