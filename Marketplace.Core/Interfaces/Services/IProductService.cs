using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface IProductService
{
    IEnumerable<Product> GetProductsByCategoryAndPage(string categoryName, int pageNumber);

    Product GetProductById(int productId);

    int GetProductsCountByCategory(string categoryName);
}