using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Enter.ENB.Core;
using Enter.ENB.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Volo.Abp;

internal class AbpApplicationWithInternalServiceProvider : EntApplicationBase, IAbpApplicationWithInternalServiceProvider
{
    public IServiceScope? ServiceScope { get; private set; }


    public AbpApplicationWithInternalServiceProvider(
         Type startupModuleType,
         IServiceCollection services
    ) : base(
            startupModuleType,
            services)
    {
        Services.AddSingleton<IAbpApplicationWithInternalServiceProvider>(this);
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
