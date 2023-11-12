using Enter.ENB.DependencyInjection;

namespace Enter.ENB.Modularity;

public interface IModuleManager : ISingletonDependency
{
    Task InitializeModulesAsync( ApplicationInitializationContext context);

    void InitializeModules(ApplicationInitializationContext context);

    Task ShutdownModulesAsync( ApplicationShutdownContext context);

    void ShutdownModules( ApplicationShutdownContext context);
}