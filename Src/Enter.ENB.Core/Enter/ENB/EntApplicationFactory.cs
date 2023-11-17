using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB;

public static class EntApplicationFactory
{
    public static async Task<IEntApplicationWithInternalServiceProvider> CreateAsync<TStartupModule>(
        Action<EntApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IEntModule
    {
        var app = Create(typeof(TStartupModule), options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    public static async Task<IEntApplicationWithInternalServiceProvider> CreateAsync(
        Type startupModuleType,
        Action<EntApplicationCreationOptions>? optionsAction = null)
    {
        var app = new EntApplicationWithInternalServiceProvider(startupModuleType, options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    public static async Task<IEntApplicationWithExternalServiceProvider> CreateAsync<TStartupModule>(
        IServiceCollection services,
        Action<EntApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IEntModule
    {
        var app = Create(typeof(TStartupModule), services, options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    public static async Task<IEntApplicationWithExternalServiceProvider> CreateAsync(
        Type startupModuleType,
        IServiceCollection services,
        Action<EntApplicationCreationOptions>? optionsAction = null)
    {
        var app = new EntApplicationWithExternalServiceProvider(startupModuleType, services, options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    public static IEntApplicationWithInternalServiceProvider Create<TStartupModule>(
        Action<EntApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IEntModule
    {
        return Create(typeof(TStartupModule), optionsAction);
    }

    public static IEntApplicationWithInternalServiceProvider Create(
        Type startupModuleType,
        Action<EntApplicationCreationOptions>? optionsAction = null)
    {
        return new EntApplicationWithInternalServiceProvider(startupModuleType, optionsAction);
    }

    public static IEntApplicationWithExternalServiceProvider Create<TStartupModule>(
        IServiceCollection services,
        Action<EntApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IEntModule
    {
        return Create(typeof(TStartupModule), services, optionsAction);
    }

    public static IEntApplicationWithExternalServiceProvider Create(
        Type startupModuleType,
        IServiceCollection services,
        Action<EntApplicationCreationOptions>? optionsAction = null)
    {
        return new EntApplicationWithExternalServiceProvider(startupModuleType, services, optionsAction);
    }
}