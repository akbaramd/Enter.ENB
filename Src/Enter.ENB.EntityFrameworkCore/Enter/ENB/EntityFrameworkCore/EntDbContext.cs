using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.EntityFrameworkCore;

public class EntDbContext : DbContext, IEntDbContext  
{
    public EntDbContext(DbContextOptions options) : base(options)
    {
        
    }
}