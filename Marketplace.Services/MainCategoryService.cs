using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.Core.Interfaces.Services;

namespace Marketplace.Services;

public class MainCategoryService : IMainCategoryService
{
    private readonly IUnitOfWork _uow;

    public MainCategoryService(IUnitOfWork uow) => _uow = uow;

    public IEnumerable<MainCategory> GetAllMainCategories() => _uow.MainCategoryRepository.GetAll();
}