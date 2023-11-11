using Enter.ENB.Core.Modularity;

namespace Enter.ENB.Modularity;

public interface IEntModule
{
    void ConfigureServices(ServiceConfigurationContext context);
    Task ConfigureServicesAsync(ServiceConfigurationContext context);
}