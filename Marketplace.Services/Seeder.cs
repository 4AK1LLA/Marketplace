using Marketplace.Core.Interfaces;

namespace Marketplace.Services;

public static class Seeder
{
    private const string path = "";
    public static void Seed(IUnitOfWork uow)
    {
        if (uow.MainCategoryRepository.Count() != 0 || uow.ProductRepository.Count() != 0)
        {
            return;
        }

        
    }
}
