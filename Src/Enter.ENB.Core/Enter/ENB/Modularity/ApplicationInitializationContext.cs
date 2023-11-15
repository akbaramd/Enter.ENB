using Enter.ENB.Statics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Enter.ENB.Modularity;

public class ApplicationInitializationContext : IServiceProviderAccessor
{
    public IServiceProvider ServiceProvider { get; set; }


    public ApplicationInitializationContext(IServiceProvider serviceProvider)
    {
        EntCheck.NotNull(serviceProvider, nameof(serviceProvider));

        ServiceProvider = serviceProvider;
       
    }
}