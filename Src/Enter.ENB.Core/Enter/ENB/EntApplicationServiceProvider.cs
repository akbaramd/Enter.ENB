using Enter.ENB.Exceptions;
using Enter.ENB.Statics;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB;

internal class EntApplicationServiceProvider : EntApplication, IEntApplicationServiceProvider
{
    private IEntApplicationServiceProvider _entApplicationServiceProviderImplementation;

    public EntApplicationServiceProvider(
        Type startupModuleType,
        IServiceCollection services
        
    ) : base(
        startupModuleType,
        services)
    {
        services.AddSingleton<IEntApplicationServiceProvider>(this);
    }

    void IEntApplicationServiceProvider.SetServiceProvider(IServiceProvider serviceProvider)
    {
        EntCheck.NotNull(serviceProvider, nameof(serviceProvider));

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ServiceProvider != null)
        {
            if (ServiceProvider != serviceProvider)
            {
                throw new EntException("Service provider was already set before to another service provider instance.");
            }

            return;
        }

        SetServiceProvider(serviceProvider);
    }

    public async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        EntCheck.NotNull(serviceProvider, nameof(serviceProvider));

        SetServiceProvider(serviceProvider);

        await InitializeModulesAsync();
    }

    public void Initialize(IServiceProvider serviceProvider)
    {
        EntCheck.NotNull(serviceProvider, nameof(serviceProvider));

        SetServiceProvider(serviceProvider);

        InitializeModules();
    }

    public override void Dispose()
    {
        base.Dispose();

        if (ServiceProvider is IDisposable disposableServiceProvider)
        {
            disposableServiceProvider.Dispose();
        }
    }
}
