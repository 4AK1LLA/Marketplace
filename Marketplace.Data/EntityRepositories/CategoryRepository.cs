using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(MarketplaceContext context) : base(context) { }

    public IEnumerable<Category> GetAllIncludingTags() => 
        _context.Categories!
        .Include(ct => ct.Tags)
        .ToList();
}