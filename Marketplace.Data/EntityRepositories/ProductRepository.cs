using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(MarketplaceContext context) : base(context) { }

    public Product GetIncludingCategoryAndTagValues(int id) =>
        GetAll()
        .AsQueryable()
        .Where(pr => pr.Id == id)
        .Include(pr => pr.Category)
        .Include(pr => pr.TagValues)
        .First();

    public IEnumerable<Product> GetByCategoryNameIncludingTagValuesAndPhotos(string name, int page) =>
        GetAll()
        .AsQueryable()
        .Where(pr => pr.Category!.Name == name)
        .OrderBy(pr => pr.Id)
        .Skip((page - 1) * 16)
        .Take(16)
        .Include(pr => pr.Photos)
        .Include(pr => pr.TagValues)!
        .ThenInclude(tv => tv.Tag);

    public int CountByCategoryName(string name) =>
        GetAll()
        .AsQueryable()
        .Where(pr => pr.Category!.Name == name)
        .Count();

}