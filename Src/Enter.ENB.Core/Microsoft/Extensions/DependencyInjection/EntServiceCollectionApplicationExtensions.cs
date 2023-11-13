using Enter.ENB;
using Enter.ENB.Modularity;

namespace Microsoft.Extensions.DependencyInjection;

public static class EntServiceCollectionApplicationExtensions
{
    public static Task<IEntApplicationServiceProvider> AddApplicationAsync<TStartupModule>(
        this IServiceCollection services)
        where TStartupModule : IEntModule
    {
        return services.AddApplicationAsync(typeof(TStartupModule));
    }

    public static Task<IEntApplicationServiceProvider> AddApplicationAsync(
        this IServiceCollection services,
        Type startupModuleType)
    {
        return EntApplicationFactory.CreateAsync(startupModuleType, services);
    }


    public static string? GetApplicationName(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IApplicationInfoAccessor>().ApplicationName;
    }


    public static string GetApplicationInstanceId(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IApplicationInfoAccessor>().InstanceId;
    }
}