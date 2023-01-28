using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Product GetIncludingCategoryAndTagValues(int id);

    IEnumerable<Product> GetByCategoryNameIncludingTagValues(string name);
}