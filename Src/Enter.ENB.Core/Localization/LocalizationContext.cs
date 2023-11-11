using Enter.ENB.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
namespace Enter.ENB.Core.Localization;

public class LocalizationContext : IServiceProviderAccessor
{
    public IServiceProvider ServiceProvider { get; }

    public IStringLocalizerFactory LocalizerFactory { get; }

    public LocalizationContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        LocalizerFactory = ServiceProvider.GetRequiredService<IStringLocalizerFactory>();
    }
}
