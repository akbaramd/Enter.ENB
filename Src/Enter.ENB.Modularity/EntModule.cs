using System.Runtime.Loader;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Modularity;

public abstract class EntModule
{
    public virtual  void PreConfigureServices(){}
    public virtual  void PostConfigureServices(){}
    public abstract void ConfigureServices(IServiceCollection services);
    
    public abstract void Initialize(IApplicationBuilder services);
    public virtual void PreInitialize(){}
    public virtual void PostInitialize(){}
}