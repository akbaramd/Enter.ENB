using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.EntityFrameworkCore.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class EntEfCoreServiceCollectionExtensions
{
    public static IServiceCollection AddEntDbContext<TDbContext>(
        this IServiceCollection services,
        Action<AbpDbContextRegistrationOptions>? action = null)
        where TDbContext : DbContext
    {
        
        var registrationOptions = new AbpDbContextRegistrationOptions(typeof(TDbContext), services);
        
        action?.Invoke(registrationOptions);
        
        services.AddDbContext<TDbContext>();
        
        new EfCoreRepositoryRegistrar(registrationOptions).AddRepositories();
        return services;
    }
    
    public static IServiceCollection AddEntDbContextConfigure(
        this IServiceCollection services, Action<DbContextOptionsBuilder<EntDbContext>> configure)
      
    {
        
        var s = new DbContextOptionsBuilder<EntDbContext>();
        configure.Invoke(s);
        services.AddScoped(x => s.Options);
        return services;
    }

  
    
}