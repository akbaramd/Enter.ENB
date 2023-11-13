using Microsoft.Extensions.DependencyInjection.Extensions;
using Enter.ENB.AutoMapper;
using Enter.ENB.ObjectMapping;

namespace Microsoft.Extensions.DependencyInjection;

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
