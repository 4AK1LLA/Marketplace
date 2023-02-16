namespace Marketplace.WebAPI.Extensions;

public static class StringExtensions
{
    public static string ToCategoryName(this string categoryRoute)
    {
        var str = categoryRoute.Replace('-', ' ');
        var categoryName = char.ToUpper(str[0]) + str.Substring(1);
        return categoryName;
    }
}
