using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data;

public class MainCategoryRepository : Repository<MainCategory>, IMainCategoryRepository
{
    public MainCategoryRepository(MarketplaceContext context) : base(context) { }

    public IEnumerable<MainCategory> GetAllIncludingSubcategories() => 
        _context.MainCategories!
        .Include(mc => mc.SubCategories)
        .ToList();
}