using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces.Services;

public interface IProductService
{
    IEnumerable<Product> GetProductsByCategory(string categoryName);
}