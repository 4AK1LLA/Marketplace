using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    IEnumerable<Product> GetByCategoryNameIncludingTagValuesAndPhotos(string name, int page);

    Product GetIncludingTagValuesAndPhotos(int id);

    int CountByCategoryName(string name);

    Product GetIncludingUsersThatLiked(int id);
}