using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    IEnumerable<Tag> GetByCategoryId(int categoryId);
}
