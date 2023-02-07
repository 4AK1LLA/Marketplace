using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;

namespace Marketplace.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _uow;
    private const int maxProductsPerPage = 16;

    public ProductService(IUnitOfWork uow) => _uow = uow;

    public IEnumerable<Product> GetProductsByCategoryAndPage(string categoryName, int pageNumber)
    {
        if (pageNumber <= 0 || (pageNumber - 1) > (GetProductsCountByCategory(categoryName) / maxProductsPerPage))
        {
            return null!;
        }

        return _uow.ProductRepository.GetByCategoryNameIncludingTagValuesAndPhotos(categoryName, pageNumber);
    }

    public int GetProductsCountByCategory(string categoryName) => 
        _uow.ProductRepository.CountByCategoryName(categoryName);
}