using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface IProductService
{
    IEnumerable<Product> GetProductsByCategoryAndPage(string categoryName, int pageNumber);
}