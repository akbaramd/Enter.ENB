using Enter.ENB.Statics;

namespace Enter.ENB.Modularity;

public class ApplicationShutdownContext
{
    public IServiceProvider ServiceProvider { get; }

    public ApplicationShutdownContext(IServiceProvider serviceProvider)
    {
        EntCheck.NotNull(serviceProvider, nameof(serviceProvider));

        ServiceProvider = serviceProvider;
    }
}
