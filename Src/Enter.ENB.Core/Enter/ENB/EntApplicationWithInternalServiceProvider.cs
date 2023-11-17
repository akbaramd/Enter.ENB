using Enter.ENB.Exceptions;
using Enter.ENB.Statics;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB;

internal class EntApplicationWithInternalServiceProvider : EntApplication, IEntApplicationWithInternalServiceProvider
{
    public IServiceScope? ServiceScope { get; private set; }

    public EntApplicationWithInternalServiceProvider(
        Type startupModuleType,
        Action<EntApplicationCreationOptions>? optionsAction
    ) : this(
        startupModuleType,
        new ServiceCollection(),
        optionsAction)
    {
    }

    private EntApplicationWithInternalServiceProvider(
         Type startupModuleType,
         IServiceCollection services,
        Action<EntApplicationCreationOptions>? optionsAction
    ) : base(
        startupModuleType,
        services,
        optionsAction)
    {
        Services.AddSingleton<IEntApplicationWithInternalServiceProvider>(this);
    }

    public IServiceProvider CreateServiceProvider()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ServiceProvider != null)
        {
            return ServiceProvider;
        }

        ServiceScope = Services.BuildServiceProviderFromFactory().CreateScope();
        SetServiceProvider(ServiceScope.ServiceProvider);

        return ServiceProvider!;
    }

    public async Task InitializeAsync()
    {
        CreateServiceProvider();
        await InitializeModulesAsync();
    }

    public void Initialize()
    {
        CreateServiceProvider();
        InitializeModules();
    }

    public override void Dispose()
    {
        base.Dispose();
        ServiceScope?.Dispose();
    }
}
