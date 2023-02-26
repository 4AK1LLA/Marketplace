using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }

    ICategoryRepository CategoryRepository { get; }

    IRepository<Photo> PhotoRepository { get; }

    IRepository<Tag> TagRepository { get; }

    IRepository<TagValue> TagValueRepository { get; }

    IMainCategoryRepository MainCategoryRepository { get; }

    IRepository<AppUser> AppUserRepository { get; }

    bool Save();
}