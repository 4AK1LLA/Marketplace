using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.Shared.Generic;

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

    public Result<bool> LikeProduct(int productId, string userStsId)
    {
        var resultIsLiked = new Result<bool>();

        AppUser user = _uow.AppUserRepository.Find(user => user.StsIdentifier == userStsId).FirstOrDefault()!;

        if (user is null)
        {
            resultIsLiked.ErrorMessage = "User not found";

            return resultIsLiked;
        }

        Product product = _uow.ProductRepository.GetIncludingUsersThatLiked(productId);

        if (product is null)
        {
            resultIsLiked.ErrorMessage = "Product not found";

            return resultIsLiked;
        }

        bool removed = product.UsersThatLiked.Remove(user);

        resultIsLiked.Value = false;

        if (!removed)
        {
            product.UsersThatLiked.Add(user);

            resultIsLiked.Value = true;
        }

        _uow.ProductRepository.Update(product);

        if (!_uow.Save())
        {
            resultIsLiked.ErrorMessage = "Error while updating like data in DB";
        }

        return resultIsLiked;
    }

    public IEnumerable<int> GetLikedProductIds(IEnumerable<Product> products, string userStsId)
    {
        AppUser user = _uow.AppUserRepository.GetByStsIdIncludingLikedProducts(userStsId);

        if (user is null)
        {
            return null!;
        }

        var ids = new List<int>();

        foreach (var pr in products)
        {
            if (pr.UsersThatLiked.Contains(user))
            {
                ids.Add(pr.Id);
            }
        }

        return ids;
    }

    public bool IsProductLiked(Product product, string userStsId)
    {
        AppUser user = _uow.AppUserRepository.GetByStsIdIncludingLikedProducts(userStsId);

        if (user is null)
        {
            return false;
        }

        return product.UsersThatLiked.Contains(user);
    }
}