using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface ITagService
{
    IEnumerable<Tag> GetTagsByCategoryId(int categoryId);
}
