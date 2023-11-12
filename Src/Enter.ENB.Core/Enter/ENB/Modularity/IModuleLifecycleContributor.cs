namespace Enter.ENB.Modularity;

public interface IModuleLifecycleContributor
{
    Task InitializeAsync(ApplicationInitializationContext context, IEntModule module);

    void Initialize(ApplicationInitializationContext context, IEntModule module);

    Task ShutdownAsync(ApplicationShutdownContext context, IEntModule module);

    void Shutdown(ApplicationShutdownContext context, IEntModule module);
}