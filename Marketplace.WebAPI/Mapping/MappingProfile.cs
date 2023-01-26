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

        CreateMap<Category, GetCategoryDto>()
            .ForMember(
                dest => dest.Route,
                opt => opt.MapFrom(src => src.Name!.ToLower().Replace(' ', '-'))
            );
        //I need help => i-need-help

        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductDto>();

        CreateMap<TagValue, TagValueDto>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Tag!.Name)
            );
    }
}