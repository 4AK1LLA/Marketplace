using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.Core.Interfaces.Services;

namespace Marketplace.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _uow;

    public ProductService(IUnitOfWork uow) => _uow = uow;

    public IEnumerable<Product> GetProductsByCategory(int categoryId) => 
        _uow.ProductRepository.GetByCategoryIdIncludingTagValues(categoryId);
}