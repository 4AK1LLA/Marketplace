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
        var lastPage = (GetProductsCountByCategory(categoryName) % maxProductsPerPage == 0)
            ? GetProductsCountByCategory(categoryName) / maxProductsPerPage
            : GetProductsCountByCategory(categoryName) / maxProductsPerPage + 1;

        if (pageNumber <= 0)
        {
            return _uow.ProductRepository.GetByCategoryNameIncludingTagValuesAndPhotos(categoryName, 1);
        }

        if (pageNumber > lastPage)
        {
            return _uow.ProductRepository.GetByCategoryNameIncludingTagValuesAndPhotos(categoryName, lastPage);
        }

        return _uow.ProductRepository.GetByCategoryNameIncludingTagValuesAndPhotos(categoryName, pageNumber);
    }

    public int GetProductsCountByCategory(string categoryName) => 
        _uow.ProductRepository.CountByCategoryName(categoryName);
}