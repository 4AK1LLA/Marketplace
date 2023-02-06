using Bogus;
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
        var mainCategories = models!.ToList();
        mainCategories.Add(CreateMainCategoryForPaging("MC for paging with 40 products", "categoryyy1", 40));
        mainCategories.Add(CreateMainCategoryForPaging("MC for paging with 128 products", "categoryyy2", 128));

        _uow.MainCategoryRepository.AddRange(mainCategories);
        _uow.Save();

        return true;
    }

    private MainCategory CreateMainCategoryForPaging(string name, string categoryName, int amountOfProducts)
    {
        Random random = new Random();
        var productsForPaging = new Faker<Product>()
            .RuleFor(pr => pr.Title, f => f.Lorem.Sentence(6))
            .RuleFor(pr => pr.PublicationDate, f => f.Date.Future())
            .RuleFor(pr => pr.Location, f => f.Address.City());

        var products = productsForPaging.Generate(amountOfProducts);

        foreach (var pr in products)
        {
            pr.TagValues = new List<TagValue>();
            pr.TagValues.Add(new TagValue
            {
                Tag = new Tag { Name = "Price" },
                Value = random.Next(1000, 10000).ToString()
            });
            pr.Photos = new List<Photo> { new Photo { IsMain = true, URL = "https://picsum.photos/700/700" } };
        }

        return new MainCategory
        {
            Name = name,
            SubCategories = new List<Category>
            {
                new Category { Name = categoryName, Products = products }
            }
        };
    }
}
