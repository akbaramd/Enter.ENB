
using Enter.ENB.Modularity;
using Enter.ENB.Threading;

namespace Enter.ENB.EntityFrameworkCore;

[DependsOnModules(typeof(EntThreadingModule))]
public class EntAuditingModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}