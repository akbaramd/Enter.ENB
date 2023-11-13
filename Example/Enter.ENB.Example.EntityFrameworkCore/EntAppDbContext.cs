using Enter.ENB.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Example.EntityFrameworkCore;

public class EntAppDbContext : EntIdentityDbContext
{
    public EntAppDbContext(DbContextOptions<EntAppDbContext> options) : base(options)
    {
        
    }
}