using AutoMapper;

namespace Enter.ENB.AutoMapper;

public interface IEntAutoMapperConfigurationContext
{
    IMapperConfigurationExpression MapperConfiguration { get; }

    IServiceProvider ServiceProvider { get; }
}
