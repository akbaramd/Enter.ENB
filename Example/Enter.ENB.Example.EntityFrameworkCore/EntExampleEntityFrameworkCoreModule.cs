using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Identity.EntityFrameworkCore;
using Enter.ENB.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Example.EntityFrameworkCore;

[DependsOnModules(
    typeof(EntIdentityEntityFrameworkCoreModule<EntAppDbContext>)
)]
public class EntExampleEntityFrameworkCoreModule : EntModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {

      
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        
        context.Services.AddEntDbContext<EntAppDbContext>(c =>
        {
            c.UseSqlServer(Configuration.GetConnectionString("Default"));
        });
        
        
    }
}