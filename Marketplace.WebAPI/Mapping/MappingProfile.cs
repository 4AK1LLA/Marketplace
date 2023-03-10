using AutoMapper;
using Marketplace.Core.Entities;
using Marketplace.WebAPI.DTO;

namespace Marketplace.WebAPI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MainCategory, MainCategoryDto>()
            .ForMember(
                dest => dest.Route,
                opt => opt.MapFrom(src => src.Name!.ToLower().Replace(' ', '-'))
            );

        CreateMap<Category, CategoryDto>()
            .ForMember(
                dest => dest.Route,
                opt => opt.MapFrom(src => src.Name!.ToLower().Replace(' ', '-'))
            );

        CreateMap<Product, ProductDto>()
            .ForMember(
                dest => dest.PriceInfo,
                opt => opt.MapFrom(src => GetPriceInfoIfExists(src))
            )
            .ForMember(
                dest => dest.Condition,
                opt => opt.MapFrom(src => GetConditionIfExists(src))
            )
            .ForMember(
                dest => dest.PublicationDate,
                opt => opt.MapFrom(src => src.PublicationDate.ToString("m")) //1 January
            )
            .ForMember(
                dest => dest.MainPhotoUrl,
                opt => opt.MapFrom(src => (src.Photos!.Count != 0) ? src.Photos!.Where(ph => ph.IsMain == true).First().URL : null)
            );

        CreateMap<Product, ProductDetailsDto>()
            .ForMember(
                dest => dest.PublicationDate,
                opt => opt.MapFrom(src => src.PublicationDate.ToString("ddd, d MMMM h:mm tt")) //Mon, 13 December 12:05 AM
            )
            .ForMember(
                dest => dest.PriceInfo,
                opt =>
                {
                    opt.SetMappingOrder(0);
                    opt.MapFrom(src => GetPriceInfoIfExists(src));
                }
            )
            .ForMember(
                dest => dest.TagValues,
                opt =>
                {
                    opt.SetMappingOrder(1);
                    opt.MapFrom(src => RemovePriceInfoTags(src));
                }
            );

        CreateMap<TagValue, TagValueDto>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Tag!.Name)
            );

        CreateMap<Photo, PhotoDto>();

        CreateMap<CreateUserDto, AppUser>();
        CreateMap<AppUser, GetUserDto>();

        CreateMap<MainCategory, MainCategoryPostDto>();
        CreateMap<Category, CategoryPostDto>();

        CreateMap<Tag, TagDto>();
    }

    private string GetPriceInfoIfExists(Product src)
    {
        if (!src.TagValues!.Any() || src.TagValues is null)
        {
            return null!;
        }

        //TODO: first try to get price, then free and exchange
        foreach (var tv in src.TagValues)
        {
            switch (tv.Tag!.Name)
            {
                case "Price":
                case "Salary":
                    return $"{tv.Value} {src.TagValues.First(tv => tv.Tag!.Name == "Currency").Value}";
                case "Free":
                case "Exchange":
                    return (tv.Value == "true") ? tv.Tag!.Name : null!;
                default:
                    break;
            }
        }

        return null!;
    }

    private string GetConditionIfExists(Product src)
    {
        if (!src.TagValues!.Any() || src.TagValues is null)
        {
            return null!;
        }

        var tv = src.TagValues.FirstOrDefault(tv => tv.Tag!.Name == "Condition");

        return (tv is not null) ? tv.Value! : null!;
    }

    private IEnumerable<TagValue> RemovePriceInfoTags(Product src)
    {
        if (!src.TagValues!.Any() || src.TagValues is null)
        {
            return null!;
        }

        var result = src.TagValues;

        string[] arr = { "Price", "Salary", "Free", "Exchange", "Currency" };

        foreach (var tv in src.TagValues)
        {
            if (arr.Contains(tv.Tag!.Name))
            {
                result.Remove(tv);
            }
        }

        return result;
    }
}