using Enter.ENB.Domain;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.DDD.Application;

[DependsOnModules(typeof(EntDddDomainModule))]
public class EnterEnbDddApplicationModule : EntModule
{
}