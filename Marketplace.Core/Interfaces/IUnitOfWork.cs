using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }

    ICategoryRepository CategoryRepository { get; }

    IRepository<Photo> PhotoRepository { get; }

    ITagRepository TagRepository { get; }

    IRepository<TagValue> TagValueRepository { get; }

    IMainCategoryRepository MainCategoryRepository { get; }

    IUserRepository AppUserRepository { get; }

    bool Save();
}