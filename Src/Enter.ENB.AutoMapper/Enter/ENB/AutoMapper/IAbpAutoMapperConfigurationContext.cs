using AutoMapper;

namespace Enter.ENB.AutoMapper.Enter.ENB.AutoMapper;

public interface IEntAutoMapperConfigurationContext
{
    IMapperConfigurationExpression MapperConfiguration { get; }

    IServiceProvider ServiceProvider { get; }
}
