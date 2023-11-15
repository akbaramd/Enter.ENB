using Enter.ENB.Domain;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class EntEfCoreServiceCollectionExtensions
{
    public static IServiceCollection AddEntDbContext<TDbContext>(
        this IServiceCollection services, Action<DbContextOptionsBuilder> configure)
        where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(configure);
        return services;
    }

    public static IServiceCollection AddRepository<TDbContext, TEntity, TKey>(
        this IServiceCollection services) where TDbContext : DbContext where TEntity : class, IEntEntity<TKey>

    {
        services.AddTransient<IRepository<TEntity, TKey>, EfCoreRepository<TDbContext, TEntity, TKey>>();
        return services;
    }
}