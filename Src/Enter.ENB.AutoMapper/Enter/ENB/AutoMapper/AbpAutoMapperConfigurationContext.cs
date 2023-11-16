using AutoMapper;

namespace Enter.ENB.AutoMapper.Enter.ENB.AutoMapper;

public class EntAutoMapperConfigurationContext : IEntAutoMapperConfigurationContext
{
    public IMapperConfigurationExpression MapperConfiguration { get; }

    public IServiceProvider ServiceProvider { get; }

    public EntAutoMapperConfigurationContext(
        IMapperConfigurationExpression mapperConfigurationExpression,
        IServiceProvider serviceProvider)
    {
        MapperConfiguration = mapperConfigurationExpression;
        ServiceProvider = serviceProvider;
    }
}
