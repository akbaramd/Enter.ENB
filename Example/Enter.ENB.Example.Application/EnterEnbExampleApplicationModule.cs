using Enter.ENB.DDD.Application;
using Enter.ENB.Example.Domain;
using Enter.ENB.Modularity;

namespace Enter.ENB.Example.Application;

[DependsOnModules(
    typeof(EnterEnbExampleDomainModule),
    typeof(EnterEnbDddApplicationModule)
    )]
public class EnterEnbExampleApplicationModule :EntModule 
{
    
  
}