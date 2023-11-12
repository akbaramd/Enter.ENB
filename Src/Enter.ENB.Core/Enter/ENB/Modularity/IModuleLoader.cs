using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Modularity;

public interface IModuleLoader
{
    IEntModuleDescriptor[] LoadModules(
        IServiceCollection services,
        Type startupModuleType
    );
}