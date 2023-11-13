using System.Reflection;
using Enter.ENB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Microsoft.Extensions.DependencyInjection;

public static class EntEfCoreServiceCollectionExtensions
{
    public static IServiceCollection AddEntDbContext<TDbContext>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> configure)
        where TDbContext : EntDbContext<TDbContext>
    {

        
        services.AddDbContext<EntDbContext<TDbContext>>(configure);
        
        
        var replacedMultiTenantDbContextTypes = typeof(TDbContext).GetCustomAttributes<AlternativeDbContextAttribute>(true)
            .Select(x => x.ReplacedDbContextTypes).ToList();
        
        foreach (var dbContextType in replacedMultiTenantDbContextTypes)
        {
            services.AddTransient(dbContextType, sp =>
            {
                var res = sp.GetRequiredService<TDbContext>();
                return res ;
            });
        }
        
        return services;
    }
}
