using Enter.ENB.AutoMapper.Enter.ENB.AutoMapper;
using Enter.ENB.ObjectMapping.Enter.ENB.ObjectMapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Enter.ENB.AutoMapper.Microsoft.Extensions.DependencyInjection;

public static class EntAutoMapperServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapperObjectMapper(this IServiceCollection services)
    {
        return services.Replace(
            ServiceDescriptor.Transient<IAutoObjectMappingProvider, AutoMapperAutoObjectMappingProvider>()
        );
    }

    public static IServiceCollection AddAutoMapperObjectMapper<TContext>(this IServiceCollection services)
    {
        return services.Replace(
            ServiceDescriptor.Transient<IAutoObjectMappingProvider<TContext>, AutoMapperAutoObjectMappingProvider<TContext>>()
        );
    }
}
