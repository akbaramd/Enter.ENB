using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Example.EntityFrameworkCore;
using Enter.ENB.Identity.Domain.Users;
using Enter.ENB.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Example.Domain;

[DependsOnModules(
    typeof(EntIdentityEntityFrameworkCoreModule)
)]
public class EntExampleEntityFrameworkCoreModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        
        Configure<EntityFrameworkCoreOptions>(options =>
        {
            context.Services.AddDbContext<EntAppDbContext>(c =>
            {
                c.UseSqlServer(options.ConnectionStrings.Default);
            });
        });
    }
}