using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.EntityFrameworkCore;

public class EntDbContext<TDbContext> : DbContext , IEntDbContext  where TDbContext : EntDbContext<TDbContext>
{
    public EntDbContext(DbContextOptions options) : base(options)
    {
        
    }
}