using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.EntityFrameworkCore;

public class EntDbContext : DbContext   
{
    public EntDbContext(DbContextOptions<EntDbContext> options) : base(options)
    {
        
    }
}