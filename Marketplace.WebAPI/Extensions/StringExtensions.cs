namespace Marketplace.WebAPI.Extensions;

public static class StringExtensions
{
    public static string ToCategoryName(this string categoryRoute)
    {
        var categoryName = categoryRoute.Replace('-', ' ');
        return categoryName;
    }
}
