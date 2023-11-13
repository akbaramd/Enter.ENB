using Enter.ENB.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.EntityFrameworkCore;

public class EntDbContext<TDbContext> : DbContext , IEntDbContext , ITransientDependency where TDbContext : EntDbContext<TDbContext>
{
    public EntDbContext(DbContextOptions options) : base(options)
    {
        
    }
}