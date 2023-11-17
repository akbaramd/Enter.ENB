using Enter.ENB.Identity.Domain.Shared;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Identity.Domain;


[DependsOnModules(typeof(EntIdentityDomainSharedModule))]
public class EntIdentityDomainModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
        context.Services.AddEntIdentity();
    }
}