using Bogus;
using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using System.Text.Json;

namespace Marketplace.Services;

public class Seeder : ISeeder
{
    private const string _mainFilePath = "SeedData/data.json";
    private const string _tagsFilePath = "SeedData/tags.json";
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

        using (StreamReader sr = new StreamReader(_mainFilePath))
        {
            modelsJson = sr.ReadToEnd();
        }

        List<MainCategory> mainCategories = JsonSerializer.Deserialize<List<MainCategory>>(modelsJson)!;
        mainCategories.Add(CreateMainCategoryForPaging("MC for paging with 40 products", "categoryyy1", 40));
        mainCategories.Add(CreateMainCategoryForPaging("MC for paging with 128 products", "categoryyy2", 128));

        #region SEEDING_TAGS

        string tagsJson;

        using (StreamReader sr = new StreamReader(_tagsFilePath))
        {
            tagsJson = sr.ReadToEnd();
        }

        IEnumerable<Tag> tags = JsonSerializer.Deserialize<List<Tag>>(tagsJson)!;

        MainCategory mcRealty = mainCategories.First(mc => mc.Name == "Realty");

        mcRealty.SubCategories!.Add(new Category
        {
            Name = "Houses for rent",
            Tags = tags.ToList()
        });

        #endregion

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
