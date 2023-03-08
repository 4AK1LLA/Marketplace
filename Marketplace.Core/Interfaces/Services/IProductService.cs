using Marketplace.Core.Entities;
using Marketplace.Shared.Generic;

namespace Marketplace.Core.Interfaces;

public interface IProductService
{
    IEnumerable<Product> GetProductsByCategoryAndPage(string categoryName, int pageNumber);

    Product GetProductById(int productId);

    int GetProductsCountByCategory(string categoryName);

    bool CreateProductWithTagValues(Product product, IDictionary<int, string> tagIdsAndValues, int categoryId, string userStsId);

    Result<bool> LikeProduct(int productId, string userStsId);
}