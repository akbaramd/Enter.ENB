using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Identity.EntityFrameworkCore;
using Enter.ENB.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Example.EntityFrameworkCore;

[DependsOnModules(
    typeof(EntIdentityEntityFrameworkCoreModule)
)]
public class EntExampleEntityFrameworkCoreModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        
        Configure<EntityFrameworkCoreOptions>(options =>
        {
            context.Services.AddEntDbContext<EntAppDbContext>(c =>
            {
                c.UseSqlServer(options.ConnectionStrings.Default);
            });
        });
    }
}