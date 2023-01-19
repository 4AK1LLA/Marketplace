using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    IEnumerable<Category> GetAllIncludingTags();
}