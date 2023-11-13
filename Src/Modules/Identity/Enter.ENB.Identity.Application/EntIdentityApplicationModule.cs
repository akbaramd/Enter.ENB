using Enter.ENB.Domain;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Identity.Application;

[DependsOnModules(typeof(EntIdentityApplicationContractsModule))]
public class EntIdentityApplicationModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IUserAppService, UserAppService>();
    }
}