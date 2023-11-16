using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.Enb.Threading;

public class EntThreadingModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<ICancellationTokenProvider>(NullCancellationTokenProvider.Instance);
        context.Services.AddSingleton(typeof(IAmbientScopeProvider<>), typeof(AmbientDataContextAmbientScopeProvider<>));
    }
}
