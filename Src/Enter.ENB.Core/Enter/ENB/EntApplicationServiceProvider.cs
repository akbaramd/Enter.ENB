using System.Diagnostics.CodeAnalysis;
using Enter.ENB.Exceptions;
using Enter.ENB.Statics;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB;

internal class EntApplicationServiceProvider : EntApplication, IEntApplicationServiceProvider
{
    private IEntApplicationServiceProvider _entApplicationServiceProviderImplementation;

    public EntApplicationServiceProvider(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services
        
    ) : base(
        startupModuleType,
        services)
    {
        services.AddSingleton<IEntApplicationServiceProvider>(this);
    }

    void IEntApplicationServiceProvider.SetServiceProvider([NotNull] IServiceProvider serviceProvider)
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

    public void Initialize([NotNull] IServiceProvider serviceProvider)
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
