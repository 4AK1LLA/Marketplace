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
        int productsCount = GetProductsCountByCategory(categoryName);

        int lastPage = (productsCount % maxProductsPerPage == 0)
            ? productsCount / maxProductsPerPage
            : productsCount / maxProductsPerPage + 1;

        if (lastPage == 0)
        {
            return Enumerable.Empty<Product>();
        }

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

    public Product GetProductById(int productId) =>
        _uow.ProductRepository.GetIncludingTagValuesAndPhotos(productId);

    public int GetProductsCountByCategory(string categoryName) =>
        _uow.ProductRepository.CountByCategoryName(categoryName);

    public bool CreateProductWithTagValues(Product product, IDictionary<int, string> tagIdsAndValues, int categoryId, string userStsId)
    {
        if (product.Title is null || product.Description is null || product.Location is null || !tagIdsAndValues.Any())
        {
            return false;
        }

        product.PublicationDate = DateTime.Now;

        Category category = _uow.CategoryRepository.Get(categoryId)!;

        if (category is null)
        {
            return false;
        }

        product.Category = category;

        AppUser user = _uow.AppUserRepository.Find(user => user.StsIdentifier == userStsId).FirstOrDefault()!;

        if (user is null)
        {
            return false;
        }

        var tagValues = new List<TagValue>();

        foreach (var pair in tagIdsAndValues)
        {
            Tag tag = _uow.TagRepository.Get(pair.Key)!;

            if (tag is null)
            {
                return false;
            }

            tagValues.Add(new TagValue
            {
                Tag = tag,
                Value = pair.Value
            });
        }

        product.TagValues = tagValues;

        _uow.ProductRepository.Add(product);

        if (!_uow.Save())
        {
            return false;
        }

        return true;
    }

    public bool LikeProduct(int productId, string userStsId)
    {
        AppUser user = _uow.AppUserRepository.Find(user => user.StsIdentifier == userStsId).FirstOrDefault()!;

        if (user is null)
        {
            return false;
        }

        Product product = _uow.ProductRepository.GetIncludingUsersThatLiked(productId);

        if (product is null)
        {
            return false;
        }

        bool disliked = product.UsersThatLiked.Remove(user);

        if (!disliked)
        {
            product.UsersThatLiked.Add(user);
        }

        _uow.ProductRepository.Update(product);

        return _uow.Save();
    }
}