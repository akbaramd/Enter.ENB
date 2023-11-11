
using Enter.ENB.Domain;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Example.Domain;

[DependsOnModules(typeof(EnterEnbDddDomainModule))]
public class EnterEnbExampleDomainModule : EntModule 
{
   
}