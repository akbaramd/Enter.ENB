using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.Identity.Application.Contracts;
using Enter.ENB.Identity.Application.Contracts.Roles.Dtos;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Identity.Application;

[DependsOnModules(typeof(EntIdentityApplicationModule))]
public class EntIdentityApiModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}