using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(MarketplaceContext context) : base(context) { }

    public IEnumerable<Category> GetAllIncludingTags() => 
        GetAll()
        .AsQueryable()
        .Include(ct => ct.Tags);
}