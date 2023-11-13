using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Example.EntityFrameworkCore;

public class EntDbContext : DbContext
{
    public EntDbContext(DbContextOptions options) : base(options)
    {
        
    }
}