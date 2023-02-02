using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using System.Text.Json;

namespace Marketplace.Services;

public class Seeder : ISeeder
{
    private const string filePath = "SeedData/data.json";
    private readonly IUnitOfWork _uow;

    public Seeder(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public bool Seed()
    {
        if (_uow.MainCategoryRepository.Count() != 0 || _uow.ProductRepository.Count() != 0)
        {
            return false;
        }

        string modelsJson;

        using (var sr = new StreamReader(filePath))
        {
            modelsJson = sr.ReadToEnd();
        }

        var models = JsonSerializer.Deserialize<IEnumerable<MainCategory>>(modelsJson);

        _uow.MainCategoryRepository.AddRange(models!);
        _uow.Save();

        return true;
    }
}
