using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(MarketplaceContext context) : base(context) { }

    public Product GetIncludingCategoryAndTagValues(int id) =>
        _context.Products!
            .Where(pr => pr.Id == id)
            .Include(pr => pr.Category)
            .Include(pr => pr.TagValues)
            .First();

    public IEnumerable<Product> GetByCategoryIdIncludingTagValues(int categoryId) =>
        _context.Products!
            .Where(pr => pr.Category!.Id == categoryId)
            .Include(pr => pr.TagValues)!
            .ThenInclude(tv => tv.Tag)
            .ToList();
}