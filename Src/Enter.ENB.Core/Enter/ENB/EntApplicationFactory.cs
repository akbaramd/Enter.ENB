using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB;

public static class EntApplicationFactory
{
    public static  Task<IEntApplicationServiceProvider> CreateAsync<TStartupModule>(IServiceCollection services)
        where TStartupModule : IEntModule
    {
      return CreateAsync(typeof(TStartupModule),services);
    }

    public static async Task<IEntApplicationServiceProvider> CreateAsync(
        Type startupModuleType,IServiceCollection serviceCollection)
    {
        var app = new EntApplicationServiceProvider(startupModuleType, serviceCollection);
        await app.ConfigureServicesAsync();
        return app;
    }


    public static IEntApplicationServiceProvider Create<TStartupModule>(IServiceCollection services)
        where TStartupModule : IEntModule
    {
        return Create(typeof(TStartupModule),services);
    }

    public static IEntApplicationServiceProvider Create(Type startupModuleType,
        IServiceCollection serviceCollection)
    {
        var app = new EntApplicationServiceProvider(startupModuleType, serviceCollection);
        app.ConfigureServices();
        return app;
    }

}