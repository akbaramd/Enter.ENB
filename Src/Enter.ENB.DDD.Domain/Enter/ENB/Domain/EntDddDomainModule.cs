using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Modularity;

namespace Enter.ENB.Domain;

[DependsOnModules(typeof(EntAuditingModule))]
public class EntDddDomainModule : EntModule
{
}