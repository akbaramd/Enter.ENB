using Enter.ENB.Domain;
using Enter.ENB.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.DDD.Application;

[DependsOnModules(typeof(EnterEnbDddDomainModule))]
public class EnterEnbDddApplicationModule : EntModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
    }

    public override void Initialize(IApplicationBuilder services)
    {
    }
}