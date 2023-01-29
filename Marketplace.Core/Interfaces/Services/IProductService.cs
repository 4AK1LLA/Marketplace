using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface IProductService
{
    IEnumerable<Product> GetProductsByCategory(string categoryName);
}