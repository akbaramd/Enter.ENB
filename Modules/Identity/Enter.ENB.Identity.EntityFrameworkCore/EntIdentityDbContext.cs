using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Identity.EntityFrameworkCore;

public class EntIdentityDbContext : EntDbContext ,IEntIdentityDbContext
{
    public EntIdentityDbContext(DbContextOptions<EntDbContext> options): base(options)
    {
        
    }

    public DbSet<EntIdentityUser> Users { get;  }
    public DbSet<EntIdentityRole> Roles { get; }
    public DbSet<EntIdentityClaim> Claims { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureEntIdentityUser();
        modelBuilder.ConfigureEntIdentityRole();
        
        base.OnModelCreating(modelBuilder);
        
    }
}

