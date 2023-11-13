using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.EntityFrameworkCore.Repositories;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Identity.EntityFrameworkCore;


[DependsOnModules(typeof(EntEntityFrameworkCoreModule))]
public class EntIdentityEntityFrameworkCoreModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        
        
        
        context.Services.AddConventionalRegistrar(new EntIdentityEntityFrameworkConventionalRegistrar());
        context.Services.AddTransient<IEntUserRepository, EntUserRepository>();
    }
}