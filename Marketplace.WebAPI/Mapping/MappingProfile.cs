using AutoMapper;
using Marketplace.Core.Entities;
using Marketplace.WebAPI.DTO;

namespace Marketplace.WebAPI.Mapping;

public class MappingProfile : Profile
{
    private readonly string[] significantTags = { "Price", "Salary", "Condition" };

    public MappingProfile()
    {
        //Mapping routes I need help => i-need-help
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

        //Mapping only important tags and date (1 January) format
        CreateMap<Product, ProductDto>()
            .ForMember(
                dest => dest.TagValues,
                opt => opt.MapFrom(src => src.TagValues!.Where(tv =>
                    tv.Tag!.Name!.Equals(significantTags[0]) ||
                    tv.Tag!.Name!.Equals(significantTags[1]) ||
                    tv.Tag!.Name!.Equals(significantTags[2])
                ))
            )
            .ForMember(
                dest => dest.PublicationDate,
                opt => opt.MapFrom(src => src.PublicationDate.ToString("m"))
            )
            .ForMember(
                dest => dest.MainPhotoUrl,
                opt => opt.MapFrom(src => (src.Photos!.Count != 0) ? src.Photos!.Where(ph => ph.IsMain == true).First().URL : null)
            );

        CreateMap<Product, DetailProductDto>()
            .ForMember(
                dest => dest.PublicationDate,
                opt => opt.MapFrom(src => src.PublicationDate.ToString("m"))
            );

        //Mapping tagvalues
        CreateMap<TagValue, TagValueDto>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Tag!.Name)
            );

        CreateMap<Photo, PhotoDto>();
    }
}