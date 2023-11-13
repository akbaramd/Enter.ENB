using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using Enter.ENB.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Enter.ENB.EntityFrameworkCore;

public class EntDbContextConfigurationContext : IServiceProviderAccessor
{
    public IServiceProvider ServiceProvider { get; }

    public string ConnectionString { get; }

    public string? ConnectionStringName { get; }

    public DbConnection? ExistingConnection { get; }

    public DbContextOptionsBuilder DbContextOptions { get; protected set; }

    public EntDbContextConfigurationContext(
        [NotNull] string connectionString,
        [NotNull] IServiceProvider serviceProvider,
        string? connectionStringName,
        DbConnection? existingConnection)
    {
        ConnectionString = connectionString;
        ServiceProvider = serviceProvider;
        ConnectionStringName = connectionStringName;
        ExistingConnection = existingConnection;

        DbContextOptions = new DbContextOptionsBuilder()
            .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
            .UseApplicationServiceProvider(serviceProvider);
    }
}

public class EntDbContextConfigurationContext<TDbContext> : EntDbContextConfigurationContext
    where TDbContext : EntDbContext<TDbContext>
{
    public new DbContextOptionsBuilder<TDbContext> DbContextOptions => (DbContextOptionsBuilder<TDbContext>)base.DbContextOptions;

    public EntDbContextConfigurationContext(
        string connectionString,
        [NotNull] IServiceProvider serviceProvider,
        string? connectionStringName,
        DbConnection? existingConnection)
        : base(
            connectionString,
            serviceProvider,
            connectionStringName,
            existingConnection)
    {
        base.DbContextOptions = new DbContextOptionsBuilder<TDbContext>()
            .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
            .UseApplicationServiceProvider(serviceProvider); ;
    }
}
