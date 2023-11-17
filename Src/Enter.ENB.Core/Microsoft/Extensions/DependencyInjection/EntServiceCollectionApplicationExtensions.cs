using Enter.ENB;
using Enter.ENB.Modularity;

namespace Microsoft.Extensions.DependencyInjection;

public static class EntServiceCollectionApplicationExtensions
{
    public static Task<IEntApplicationWithExternalServiceProvider> AddApplicationAsync<TStartupModule>(
        this IServiceCollection services,
        Action<EntApplicationCreationOptions>? action = null)
        where TStartupModule : IEntModule
    {
        return services.AddApplicationAsync(typeof(TStartupModule),action);
    }

    public static Task<IEntApplicationWithExternalServiceProvider> AddApplicationAsync(
        this IServiceCollection services,
        Type startupModuleType,
        Action<EntApplicationCreationOptions>? action = null)
    {
        return EntApplicationFactory.CreateAsync(startupModuleType, services,action);
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