using Enter.ENB.Identity.Application;
using Enter.ENB.Modularity;

namespace Enter.ENB.Identity.Api;

[DependsOnModules(typeof(EntIdentityApplicationModule))]
public class EntIdentityApiModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}