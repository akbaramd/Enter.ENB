using Enter.ENB.Modularity;
using Enter.ENB.Uow;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Domain;

[DependsOnModules(typeof(EnterEnbUowModule))]
public class EnterEnbDddDomainModule : EntModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
    }

    public override void Initialize(IApplicationBuilder services)
    {
    }
}