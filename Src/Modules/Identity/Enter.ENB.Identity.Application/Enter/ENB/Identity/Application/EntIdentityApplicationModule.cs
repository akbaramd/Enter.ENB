using Enter.ENB.Domain;
using Enter.ENB.Modularity;

namespace Enter.ENB.Identity.Application;

[DependsOnModules(typeof(EntDddDomainModule))]
public class EntIdentityApplicationModule : EntModule
{
}