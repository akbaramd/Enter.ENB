using Enter.ENB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Enter.ENB.Example.EntityFrameworkCore;

public class EntAppFactoryDbContext : IDesignTimeDbContextFactory<EntAppDbContext>
{
    public EntAppDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<EntDbContext>()
            .UseSqlServer("Server=DESKTOP-4GKO3R7;Database=EntExampleApi;Trusted_Connection=True;TrustServerCertificate=True");
        
        return new EntAppDbContext(builder.Options);
    }

}