using Enter.ENB.Example.Domain;
using Enter.ENB.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Example.Application;

[DependsOnModules(
    typeof(EnterEnbExampleDomainModule)
    )]
public class EnterEnbExampleApplicationModule :EntModule 
{
    public override void ConfigureServices(IServiceCollection services)
    {
    }

    public override void Initialize(IApplicationBuilder services)
    {
    }
}