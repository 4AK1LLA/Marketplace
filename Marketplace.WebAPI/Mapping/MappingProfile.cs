using AutoMapper;
using Marketplace.Core.Entities;
using Marketplace.WebAPI.DTO;

namespace Marketplace.WebAPI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MainCategoryDto, MainCategory>();
        CreateMap<MainCategory, MainCategoryDto>();

        CreateMap<GetCategoryDto, Category>();
        CreateMap<Category, GetCategoryDto>();

        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductDto>();

        CreateMap<TagValue, TagValueDto>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Tag!.Name)
            );
    }
}