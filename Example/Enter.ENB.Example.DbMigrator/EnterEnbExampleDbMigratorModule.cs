using Enter.ENB.Example.EntityFrameworkCore;
using Enter.ENB.Modularity;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Example.DbMigrator;

[DependsOnModules(
    typeof(EntExampleEntityFrameworkCoreModule)
)]
public class EnterEnbExampleDbMigratorModule : EntModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {

        context.Services.AddEntDbContextConfigure(c =>
        {
            c.UseSqlServer(Configuration.GetConnectionString("Default"));
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddEntDbContext<EntAppDbContext>();
    }

    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var logger = context.ServiceProvider.GetRequiredService<ILogger<EnterEnbExampleDbMigratorModule>>();
        var dbContext =  context.ServiceProvider.GetRequiredService<EntAppDbContext>();
        
        var migration = await dbContext.Database.GetPendingMigrationsAsync();
        if (migration.Any())
        {
            logger.LogInformation("We found new migrations on project");
            
            await dbContext.Database.MigrateAsync();
            
            logger.LogInformation("All migration have ben migrated");
        }

        else
        {
            logger.LogWarning("Not found any new migrations on project");
        }
    }

}