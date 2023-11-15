using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Identity.EntityFrameworkCore;

public class EntIdentityDbContext : EntDbContext , IEntIdentityDbContext
{
    public EntIdentityDbContext(DbContextOptions options): base(options)
    {
        
    }

    public DbSet<EntUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureEntUser();
        
        base.OnModelCreating(modelBuilder);
        
    }
}