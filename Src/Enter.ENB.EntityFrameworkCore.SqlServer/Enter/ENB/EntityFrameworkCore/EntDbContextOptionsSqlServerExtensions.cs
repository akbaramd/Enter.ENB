using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Enter.ENB.EntityFrameworkCore;

public static class EntDbContextOptionsSqlServerExtensions
{
    public static void UseSqlServer(
        this EntDbContextOptions options,
        Action<SqlServerDbContextOptionsBuilder>? sqlServerOptionsAction = null)
    {
        options.Configure(context =>
        {
            context.UseSqlServer(sqlServerOptionsAction);
        });
    }

    public static void UseSqlServer<TDbContext>(
        this EntDbContextOptions options,
        Action<SqlServerDbContextOptionsBuilder>? sqlServerOptionsAction = null)
        where TDbContext : EntDbContext<TDbContext>
    {
        options.Configure<TDbContext>(context =>
        {
            context.UseSqlServer(sqlServerOptionsAction);
        });
    }
}
