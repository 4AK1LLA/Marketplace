using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;

namespace Marketplace.Services;

public class TagService : ITagService
{
    private readonly IUnitOfWork _uow;

    public TagService(IUnitOfWork uow) => _uow = uow;

    public IEnumerable<Tag> GetTagsByCategoryId(int categoryId) 
    { 
        if (categoryId <= 0)
        {
            return _uow.TagRepository.GetByCategoryId(1);
        }

        return _uow.TagRepository.GetByCategoryId(categoryId);
    }
}
