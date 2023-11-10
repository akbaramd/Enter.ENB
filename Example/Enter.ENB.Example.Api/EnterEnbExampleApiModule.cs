using Enter.ENB.Example.Application;
using Enter.ENB.Modularity;

namespace Enter.ENB.Example.Api;

[DependsOnModules(typeof(EnterEnbExampleApplicationModule))]
public class EnterEnbExampleApiModule : EntModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
    }

    public override void Initialize(IApplicationBuilder services)
    {
    }
}