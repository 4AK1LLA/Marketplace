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

        CreateMap<CreateProductDto, Product>();
    }
}