using System.Diagnostics.CodeAnalysis;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

namespace Enter.ENB.Core;

public static class AbpApplicationFactory
{
    public async static Task<IAbpApplicationWithInternalServiceProvider> CreateAsync<TStartupModule>(IServiceCollection services)
        where TStartupModule : IEntModule
    {
        var app = Create(typeof(TStartupModule),services);
         app.ConfigureServices();
        return app;
    }

    public async static Task<IAbpApplicationWithInternalServiceProvider> CreateAsync(
        Type startupModuleType,IServiceCollection serviceCollection)
    {
        var app = new AbpApplicationWithInternalServiceProvider(startupModuleType, serviceCollection);
         app.ConfigureServices();
        return app;
    }


    public static IAbpApplicationWithInternalServiceProvider Create<TStartupModule>(IServiceCollection services)
        where TStartupModule : IEntModule
    {
        return Create(typeof(TStartupModule),services);
    }

    public static IAbpApplicationWithInternalServiceProvider Create(Type startupModuleType,
        IServiceCollection serviceCollection)
    {
        return new AbpApplicationWithInternalServiceProvider(startupModuleType, serviceCollection);
    }

}