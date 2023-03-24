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

    IEnumerable<int> GetLikedProductIds(IEnumerable<Product> products, string userStsId);

    bool IsProductLiked(Product product, string userStsId);

    IEnumerable<Product> GetLikedProducts(string userStsId);

    string GetPriceInfoIfExists(Product product, bool removeTags = false);

    string GetConditionIfExists(Product product);
}