using AutoMapper;
using Marketplace.Core.Interfaces;
using Marketplace.Data;
using Marketplace.Data.Options;
using Marketplace.Services;
using Marketplace.WebAPI.Mapping;

namespace Marketplace.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services) => 
        services.AddScoped<IUnitOfWork, UnitOfWork>();

    public static IServiceCollection AddMyServices(this IServiceCollection services)
    {
        services.AddScoped<ISeeder, Seeder>();
        services.AddScoped<IMainCategoryService, MainCategoryService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }

    public static IServiceCollection AddMyDbContext(this IServiceCollection services) => 
        services.AddDbContext<MarketplaceContext>();

    public static IServiceCollection AddAutoMapper(this IServiceCollection services) =>
        services.AddSingleton(new Mapper(
            new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()))) as IMapper
            );

    public static IServiceCollection AddMyCors(this IServiceCollection services, string myAllowSpecificOrigins)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                name: myAllowSpecificOrigins, 
                policy => policy.WithOrigins("http://localhost:4200"));
        });

        return services;
    }

    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbConnectionOptions>(configuration.GetSection(DbConnectionOptions.Position));

        return services;
    }
}