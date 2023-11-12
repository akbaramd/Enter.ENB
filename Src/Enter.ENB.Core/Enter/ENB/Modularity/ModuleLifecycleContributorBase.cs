namespace Enter.ENB.Modularity;

public abstract class ModuleLifecycleContributorBase : IModuleLifecycleContributor
{
    public virtual Task InitializeAsync(ApplicationInitializationContext context, IEntModule module)
    {
        return Task.CompletedTask;
    }

    public virtual void Initialize(ApplicationInitializationContext context, IEntModule module)
    {
    }

    public virtual Task ShutdownAsync(ApplicationShutdownContext context, IEntModule module)
    {
        return Task.CompletedTask;
    }

    public virtual void Shutdown(ApplicationShutdownContext context, IEntModule module)
    {
    }
}
