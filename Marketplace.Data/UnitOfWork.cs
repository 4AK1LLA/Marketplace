using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;

namespace Marketplace.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly MarketplaceContext _context;

    public UnitOfWork(MarketplaceContext context)
    {
        _context = context;

        _context.Database.EnsureCreated();

        ProductRepository = new Repository<Product>(context);
        CategoryRepository = new CategoryRepository(context);
        PhotoRepository = new Repository<Photo>(context);
    }

    public IRepository<Product> ProductRepository { get; private set; }

    public ICategoryRepository CategoryRepository { get; private set; }

    public IRepository<Photo> PhotoRepository { get; private set; }

    public bool Save() => _context.SaveChanges() > 0;
}