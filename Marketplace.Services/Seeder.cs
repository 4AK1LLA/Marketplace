using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.Services.SerializationModels;
using Marketplace.Shared.Constants;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Marketplace.Services;

public class Seeder : ISeeder
{
    private const string sectionName = "ResourceNames";

    private readonly IUnitOfWork _uow;
    private readonly ISerializator _serializator;
    private readonly IConfiguration _configuration;

    private bool AnyData =>
        _uow.MainCategoryRepository.Count() != 0 || 
        _uow.ProductRepository.Count() != 0 || 
        _uow.AppUserRepository.Count() != 0;

    public Seeder(IUnitOfWork uow, ISerializator serializator, IConfiguration configuration)
    {
        _uow = uow;
        _serializator = serializator;
        _configuration = configuration;
    }

    public bool Seed()
    {
        if (AnyData)
        {
            return false;
        }

        var assembly = Assembly.GetExecutingAssembly();

        var resources =
            _configuration
            .GetRequiredSection(sectionName)
            .GetChildren()
            .ToDictionary(section => section.Key, section => assembly.GetManifestResourceStream(section.Value!));

        var tagModels = _serializator.Deserialize<List<TagModel>>(resources["Tags"]!);
        var mcModels = _serializator.Deserialize<List<MainCategoryModel>>(resources["Categories"]!);
        var productModels = _serializator.Deserialize<List<ProductModel>>(resources["Products"]!);

        var tags = MapTags(tagModels);
        var mainCategories = MapMainCategories(mcModels, tags);
        //var products = Map();

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

        AppUser testUser = new AppUser
        {
            StsIdentifier = AppConstants.DefaultUserId,
            UserName = AppConstants.DefaultUserEmail,
            Email = AppConstants.DefaultUserEmail,
            DisplayName = "Test User",
            PhoneNumber = "+380661234567",
            ProfilePictureUrl = "https://res.cloudinary.com/dfs6whqzs/image/upload/v1680092719/users/test-user-profile-picture_dvlavz.jpg"
        };

        _uow.AppUserRepository.Add(testUser);

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
