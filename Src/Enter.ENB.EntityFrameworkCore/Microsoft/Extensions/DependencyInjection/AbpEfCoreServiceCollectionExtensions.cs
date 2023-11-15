using Enter.ENB.Domain;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Domain.Repository;
using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class EntEfCoreServiceCollectionExtensions
{
    public static IServiceCollection AddEntDbContext<TDbContext>(
        this IServiceCollection services)
        where TDbContext : DbContext
    {
        
        var option = services.BuildServiceProvider().CreateScope().ServiceProvider.GetService<IOptions<DbContextOptionsBuilder<EntDbContext>>>();
        if (option == null)
        {
            throw new EntException("DbContextOptionsBuilder is null . please use AddEntDbContextConfigure<TDbContext> in Module PreConfigureServices Method");
        }
        services.AddDbContext<TDbContext>();
        return services;
    }
    
    public static IServiceCollection AddEntDbContextConfigure(
        this IServiceCollection services, Action<DbContextOptionsBuilder> configure)
      
    {
        
        var s = new DbContextOptionsBuilder<EntDbContext>();
        configure.Invoke(s);
        services.AddScoped(x => s.Options);
        return services;
    }

  
    
}