using Enter.ENB.Identity.EntityFrameworkCore;
using Enter.ENB.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Example.EntityFrameworkCore;

[DependsOnModules(
    typeof(EntIdentityEntityFrameworkCoreModule)
)]
public class EntExampleEntityFrameworkCoreModule : EntModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddEntDbContextConfigure(c =>
        {
            c.UseSqlServer(context.Services.GetConfiguration().GetConnectionString("Default"));
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        
        context.Services.AddEntDbContext<EntAppDbContext>();
    }
}