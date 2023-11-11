using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Core;

public interface IEntApplication: IModuleContainer,IApplicationInfoAccessor,IDisposable
{
    IServiceCollection Services { get; }
    IServiceProvider ServiceProvider { get; }
    
    Type StartupModuleType { get; }
    void ConfigureServices();
    Task Shutdown();
}