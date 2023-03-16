namespace Marketplace.Services.SerializationModels;

internal class MainCategoryModel
{
    public string? Name { get; set; }

    public string? PhotoUrl { get; set; }

    public List<CategoryModel>? SubCategories { get; set; }
}
