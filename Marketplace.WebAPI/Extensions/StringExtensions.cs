namespace Marketplace.WebAPI.Extensions;

public static class StringExtensions
{
    public static string ToCategoryName(this string categoryRoute) => categoryRoute.Replace('-', ' ');
}
