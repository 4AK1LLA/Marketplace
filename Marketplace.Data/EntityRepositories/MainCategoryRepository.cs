using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data;

public class MainCategoryRepository : Repository<MainCategory>, IMainCategoryRepository
{
    public MainCategoryRepository(MarketplaceContext context) : base(context) { }

    public IEnumerable<MainCategory> GetAllIncludingSubcategories() => 
        GetAll()
        .AsQueryable()
        .Include(mc => mc.SubCategories);
}