﻿using Bogus;
using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.Services.SerializationModels;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Marketplace.Services;

public class Seeder : ISeeder
{
    private const string sectionName = "SeedFilePaths";

    private readonly IUnitOfWork _uow;
    private readonly ISerializator _serializator;
    private readonly IConfiguration _configuration;

    public Seeder(IUnitOfWork uow, ISerializator serializator, IConfiguration configuration)
    {
        _uow = uow;
        _serializator = serializator;
        _configuration = configuration;
    }

    public bool Seed()
    {
        if (_uow.MainCategoryRepository.Count() != 0 || _uow.ProductRepository.Count() != 0)
        {
            return false;
        }

        var filePaths =
            _configuration
            .GetRequiredSection(sectionName)
            .GetChildren()
            .ToDictionary(section => section.Key, section => section.Value)!;

        var tagModels = _serializator.Deserialize<List<TagModel>>(filePaths["Tags"]!);

        IEnumerable<Tag> tags = MapTags(tagModels);
        
        //_uow.TagRepository.AddRange(tags);

        //_uow.MainCategoryRepository.Add(mc);




        //string modelsJson;

        //using (StreamReader sr = new StreamReader(""))
        //{
        //    modelsJson = sr.ReadToEnd();
        //}

        //List<MainCategory> mainCategories = JsonSerializer.Deserialize<List<MainCategory>>(modelsJson)!;
        //mainCategories.Add(CreateMainCategoryForPaging("MC for paging with 40 products", "categoryyy1", 40));
        //mainCategories.Add(CreateMainCategoryForPaging("MC for paging with 128 products", "categoryyy2", 128));

        //#region SEEDING_TAGS

        //string tagsJson;

        //using (StreamReader sr = new StreamReader(""))
        //{
        //    tagsJson = sr.ReadToEnd();
        //}

        //IEnumerable<Tag> tags1 = JsonSerializer.Deserialize<List<Tag>>(tagsJson)!;

        //MainCategory mcRealty = mainCategories.First(mc => mc.Name == "Realty");

        //mcRealty.SubCategories!.Add(new Category
        //{
        //    Name = "Houses for rent",
        //    Tags = tags.ToList()
        //});

        //#endregion

        //_uow.MainCategoryRepository.AddRange(mainCategories);

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
            pr.TagValues = new List<TagValue>
            {
                new TagValue
                {
                    Tag = new Tag { Name = "Price" },
                    Value = random.Next(1000, 10000).ToString()
                },
                new TagValue { Tag = new Tag { Name = "Currency" }, Value = "uah" }
            };
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

    private IEnumerable<Tag> MapTags(IEnumerable<TagModel> tagModels)
    {
        var tags = new List<Tag>();

        foreach (var model in tagModels)
        {
            tags.Add(new Tag
            {
                Identifier = model.Identifier,
                Name = model.Name,
                IsRequired = model.IsRequired,
                Type = model.Type,
                Remark = model.Remark,
                PossibleValues = (model.PossibleValues is not null) 
                    ? model.PossibleValues.Select(val => new PossibleValue { Value = val }).ToList() 
                    : null
            });
        }

        return tags;
    }
}
