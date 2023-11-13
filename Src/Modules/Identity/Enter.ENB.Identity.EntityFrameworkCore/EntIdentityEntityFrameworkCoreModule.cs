using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.EntityFrameworkCore.Repositories;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Identity.EntityFrameworkCore;


[DependsOnModules(typeof(EntEntityFrameworkCoreModule))]
public class EntIdentityEntityFrameworkCoreModule : EntModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new EntIdentityEntityFrameworkConventionalRegistrar());

        context.Services.AddEntDbContext<EntIdentityDbContext>();
        
        context.Services.AddTransient<IEntUserRepository, EntUserRepository>();
        base.PreConfigureServices(context);
    }
}