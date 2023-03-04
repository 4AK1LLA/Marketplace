using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data;

public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(MarketplaceContext context) : base(context) { }

    public IEnumerable<Tag> GetByCategoryId(int categoryId) =>
        GetAll()
        .AsQueryable()
        .Include(tg => tg.Categories)
        .Where(tg => tg.Categories!
        .Any(ct => ct.Id == categoryId));
}
