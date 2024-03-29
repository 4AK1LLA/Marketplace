﻿using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;

namespace Marketplace.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly MarketplaceContext _context;

    public UnitOfWork(MarketplaceContext context)
    {
        _context = context;

        _context.Database.EnsureCreated();

        ProductRepository = new ProductRepository(context);
        CategoryRepository = new CategoryRepository(context);
        PhotoRepository = new Repository<Photo>(context);
        TagRepository = new TagRepository(context);
        TagValueRepository = new Repository<TagValue>(context);
        MainCategoryRepository = new MainCategoryRepository(context);
        AppUserRepository = new UserRepository(context);
    }

    public IProductRepository ProductRepository { get; private set; }

    public ICategoryRepository CategoryRepository { get; private set; }

    public IRepository<Photo> PhotoRepository { get; private set; }

    public ITagRepository TagRepository { get; private set; }

    public IRepository<TagValue> TagValueRepository { get; private set; }

    public IMainCategoryRepository MainCategoryRepository { get; private set; }

    public IUserRepository AppUserRepository { get; private set; }

    public bool Save() => _context.SaveChanges() > 0;
}