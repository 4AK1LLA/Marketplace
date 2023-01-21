using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces.Services;

public interface IMainCategoryService
{
    IEnumerable<MainCategory> GetAllMainCategories();

    void SeedMainCategoryData();
}