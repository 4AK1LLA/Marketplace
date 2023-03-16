using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.Services.SerializationModels;
using Microsoft.Extensions.Configuration;

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
        var mcModels = _serializator.Deserialize<List<MainCategoryModel>>(filePaths["Categories"]!);

        IEnumerable<Tag> tags = MapTags(tagModels);
        IEnumerable<MainCategory> mainCategories = MapMainCategories(mcModels, tags);

        _uow.TagRepository.AddRange(tags);

        try
        {
            _uow.MainCategoryRepository.AddRange(mainCategories);
        }
        catch
        {
            //TODO: Log that there is error, most likely caused by tag ids difference in seed data
            return false;
        }

        _uow.Save();

        return true;
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

    private IEnumerable<MainCategory> MapMainCategories(IEnumerable<MainCategoryModel> mcModels, IEnumerable<Tag> tags)
    {
        var mainCategories = new List<MainCategory>();

        foreach (var mcModel in mcModels)
        {
            mainCategories.Add(new MainCategory
            {
                Name = mcModel.Name,
                PhotoUrl = mcModel.PhotoUrl,
                SubCategories = mcModel.SubCategories!
                    .Select(ctModel => new Category
                    {
                        Name = ctModel.Name,
                        Tags = ctModel.TagIds!
                            .Select(tagId => tags.FirstOrDefault(tag => tag.Identifier == tagId)).ToList()!
                    }).ToList()
            });
        }

        return mainCategories;
    }
}
