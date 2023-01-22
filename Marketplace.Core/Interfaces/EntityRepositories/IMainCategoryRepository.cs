using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface IMainCategoryRepository : IRepository<MainCategory>
{
    IEnumerable<MainCategory> GetAllIncludingSubcategories();
}