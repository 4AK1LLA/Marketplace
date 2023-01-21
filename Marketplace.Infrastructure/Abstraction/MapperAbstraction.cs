using AutoMapper;

namespace Marketplace.Infrastructure.Abstraction;

public class MapperAbstraction : Mapper, IMapperAbstraction
{
    public MapperAbstraction(IConfigurationProvider configuration) : base(configuration)
    {

    }
}