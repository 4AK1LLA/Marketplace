using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;

namespace Marketplace.Data;

public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(MarketplaceContext context) : base(context) { }

    public IEnumerable<Tag> GetByCategoryId(int categoryId) =>
        GetAll()
        .Where(tg => tg.Categories!
        .Any(ct => ct.Id == categoryId));
}
