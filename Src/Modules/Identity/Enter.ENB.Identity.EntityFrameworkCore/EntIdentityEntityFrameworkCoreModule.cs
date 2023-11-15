using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.EntityFrameworkCore.Repositories;
using Enter.ENB.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Identity.EntityFrameworkCore;


[DependsOnModules(typeof(EntEntityFrameworkCoreModule))]
public class EntIdentityEntityFrameworkCoreModule<TDbContext> : EntModule where TDbContext : DbContext
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new EntIdentityEntityFrameworkConventionalRegistrar());

        base.PreConfigureServices(context);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddRepository<TDbContext, EntUser, Guid>();
    }
}