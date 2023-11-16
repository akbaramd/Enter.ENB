using Enter.ENB.Modularity;
using Enter.Enb.Threading;

namespace Enter.ENB.Auditing;

[DependsOnModules(typeof(EntThreadingModule))]
public class EntAuditingModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}