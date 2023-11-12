using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB;

public interface IEntApplication: IModuleContainer,IApplicationInfoAccessor,IDisposable
{
    IServiceCollection Services { get; }
    IServiceProvider ServiceProvider { get; }
    
    Type StartupModuleType { get; }
    void ConfigureServices();
    Task ConfigureServicesAsync();
    Task ShutdownAsync();
    void Shutdown();
}