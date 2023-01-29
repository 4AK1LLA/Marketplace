using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface IMainCategoryService
{
    IEnumerable<MainCategory> GetAllMainCategories();
}