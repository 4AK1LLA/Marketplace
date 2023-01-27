using AutoMapper;
using Marketplace.Core.Entities;
using Marketplace.WebAPI.DTO;

namespace Marketplace.WebAPI.Mapping;

public class MappingProfile : Profile
{
    private readonly string[] significantTags = { "Price", "Salary", "Condition" };

    public MappingProfile()
    {
        CreateMap<MainCategory, MainCategoryDto>()
            .ForMember(
                dest => dest.Route,
                opt => opt.MapFrom(src => src.Name!.ToLower().Replace(' ', '-'))
            );

        CreateMap<Category, GetCategoryDto>()
            .ForMember(
                dest => dest.Route,
                opt => opt.MapFrom(src => src.Name!.ToLower().Replace(' ', '-'))
            );
        //I need help => i-need-help

        CreateMap<Product, ProductDto>()
            .ForMember(
                dest => dest.TagValues,
                opt => opt.MapFrom(src => src.TagValues!.Where(tv =>
                    tv.Tag!.Name!.Equals(significantTags[0]) ||
                    tv.Tag!.Name!.Equals(significantTags[1]) ||
                    tv.Tag!.Name!.Equals(significantTags[2])
                ))
            );

        CreateMap<TagValue, TagValueDto>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Tag!.Name)
            );
    }
}