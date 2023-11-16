using Enter.ENB.Auditing;
using Enter.ENB.Modularity;

namespace Enter.ENB.Domain;

[DependsOnModules(typeof(EntAuditingModule))]
public class EntDddDomainModule : EntModule
{
}