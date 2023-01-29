namespace Marketplace.Data.Options;

public class DbConnectionOptions
{
    public const string Position = "ConnectionStrings";

    public string DefaultConnection { get; set; } = string.Empty;
}