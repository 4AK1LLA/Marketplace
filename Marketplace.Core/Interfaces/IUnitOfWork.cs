using Marketplace.Core.Entities;

namespace Marketplace.Core.Interfaces;

public interface IUnitOfWork
{
    IRepository<Product> ProductRepository { get; }

    IRepository<Category> CategoryRepository { get; }

    IRepository<Photo> PhotoRepository { get; }

    bool Save();
}