using AutoMapper;
using Marketplace.Infrastructure.Abstraction;
using Marketplace.Infrastructure.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Infrastructure;

public static class ExtensionMethods
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));

        services.AddSingleton(new MapperAbstraction(mapperConfig));

        return services;
    }
}