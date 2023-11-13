using Enter.ENB.Exceptions;
using Enter.ENB.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Enter.ENB.EntityFrameworkCore;

public static class DbContextOptionsFactory
{
    public static DbContextOptions<TDbContext> Create<TDbContext>(IServiceProvider serviceProvider , string connectionString)
        where TDbContext : EntDbContext<TDbContext>
    {
    

        var context = new EntDbContextConfigurationContext<TDbContext>(
            connectionString,
            serviceProvider,
            "default",
            null
        );

        var options = GetDbContextOptions<TDbContext>(serviceProvider);

        PreConfigure(options, context);
        Configure(options, context);

        return context.DbContextOptions.Options;
    }

    private static void PreConfigure<TDbContext>(
        EntDbContextOptions options,
        EntDbContextConfigurationContext<TDbContext> context)
        where TDbContext : EntDbContext<TDbContext>
    {
        foreach (var defaultPreConfigureAction in options.DefaultPreConfigureActions)
        {
            defaultPreConfigureAction.Invoke(context);
        }

        var preConfigureActions = options.PreConfigureActions.GetOrDefault(typeof(TDbContext));
        if (!preConfigureActions.IsNullOrEmpty())
        {
            foreach (var preConfigureAction in preConfigureActions!)
            {
                ((Action<EntDbContextConfigurationContext<TDbContext>>)preConfigureAction).Invoke(context);
            }
        }
    }

    private static void Configure<TDbContext>(
        EntDbContextOptions options,
        EntDbContextConfigurationContext<TDbContext> context)
        where TDbContext : EntDbContext<TDbContext>
    {
        var configureAction = options.ConfigureActions.GetOrDefault(typeof(TDbContext));
        if (configureAction != null)
        {
            ((Action<EntDbContextConfigurationContext<TDbContext>>)configureAction).Invoke(context);
        }
        else if (options.DefaultConfigureAction != null)
        {
            options.DefaultConfigureAction.Invoke(context);
        }
        else
        {
            throw new EntException(
                $"No configuration found for {typeof(DbContext).AssemblyQualifiedName}! Use services.Configure<EntDbContextOptions>(...) to configure it.");
        }
    }

    private static EntDbContextOptions GetDbContextOptions<TDbContext>(IServiceProvider serviceProvider)
        where TDbContext : EntDbContext<TDbContext>
    {
        return serviceProvider.GetRequiredService<IOptions<EntDbContextOptions>>().Value;
    }

   
  
}
