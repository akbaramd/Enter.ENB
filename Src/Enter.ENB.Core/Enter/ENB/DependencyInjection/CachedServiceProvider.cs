namespace Enter.ENB.DependencyInjection;

[ExposeServices(typeof(ICachedServiceProvider))]
public class CachedServiceProvider : 
    CachedServiceProviderBase,
    ICachedServiceProvider,
    IScopedDependency
{
    public CachedServiceProvider(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }
}