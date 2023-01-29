namespace Marketplace.WebAPI.Options;

public class CorsOptions
{
    public const string Position = "AllowedOrigins";

    public string AllowedOrigins { get; set; } = string.Empty;
}
