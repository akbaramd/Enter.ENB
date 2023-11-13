using System;
using System.Linq;
using System.Reflection;
using Enter.ENB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace Microsoft.Extensions.DependencyInjection;

public static class EntEfCoreServiceCollectionExtensions
{
    public static IServiceCollection AddEntDbContext<TDbContext>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> configure)
        where TDbContext : EntDbContext<TDbContext>
    {
        services.AddDbContext<TDbContext>(configure);
        return services;
    }
}
