using Enter.ENB;
using Enter.ENB.Extensions;
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
        services.AddSingleton<IModuleManager, ModuleManager>();
        services.AddTransient< OnApplicationShutdownModuleLifecycleContributor>();
        services.AddTransient< OnPostApplicationInitializationModuleLifecycleContributor>();
        services.AddTransient< OnPreApplicationInitializationModuleLifecycleContributor>();
        services.AddTransient< OnApplicationInitializationModuleLifecycleContributor>();

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