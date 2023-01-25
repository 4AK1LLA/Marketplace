using AutoMapper;
using Marketplace.Core.DTO;
using Marketplace.Core.Entities;

namespace Marketplace.Infrastructure.AutoMapper;

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