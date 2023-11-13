using Enter.ENB.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Enter.ENB.Example.EntityFrameworkCore;

public class EntAppFactoryDbContext : IDesignTimeDbContextFactory<EntAppDbContext>
{
    public EntAppDbContext CreateDbContext(string[] args)
    {
        
        var builder = new DbContextOptionsBuilder<EntAppDbContext>()
            .UseSqlServer("Server=FAVA-A;Database=EntExampleApi;Trusted_Connection=True;TrustServerCertificate=True");

        return new EntAppDbContext(builder.Options);
    }

}