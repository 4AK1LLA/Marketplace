using AutoMapper;
using Marketplace.Core.Interfaces;
using Marketplace.Data;
using Marketplace.Data.Options;
using Marketplace.Services;
using Marketplace.WebAPI.Filters;
using Marketplace.WebAPI.Mapping;
using Marketplace.WebAPI.Options;
using Microsoft.OpenApi.Models;

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
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITagService, TagService>();

        return services;
    }

    public static IServiceCollection AddMyDbContext(this IServiceCollection services) =>
        services.AddDbContext<MarketplaceContext>();

    public static IServiceCollection AddAutoMapper(this IServiceCollection services) =>
        services.AddSingleton(new Mapper(
            new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()))) as IMapper
            );

    public static IServiceCollection AddMyCors(this IServiceCollection services, IConfiguration configuration, string myAllowSpecificOrigins)
    {
        var origins = configuration.Get<CorsOptions>().AllowedOrigins.Split(";");

        services.AddCors(options =>
        {
            options.AddPolicy(
                name: myAllowSpecificOrigins,
                policy => policy.WithOrigins(origins).AllowAnyHeader());
        });

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. **_'Bearer '_** and then access token in the text input below.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        };

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new List<string>()
            }
        };

        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Marketplace WebAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", securityScheme);
            opt.OperationFilter<AuthResponsesOperationFilter>();
        });

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", opt =>
            {
                opt.Authority = "https://localhost:7028/";
                opt.Audience = "Marketplace.WebAPI";
            });

        return services;
    }

    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbConnectionOptions>(configuration.GetSection(DbConnectionOptions.Position));

        return services;
    }
}