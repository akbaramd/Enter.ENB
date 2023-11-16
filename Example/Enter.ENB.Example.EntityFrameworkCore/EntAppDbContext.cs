using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Example.EntityFrameworkCore;

// [AlternativeDbContext(typeof(EntIdentityDbContext))]
public class EntAppDbContext : EntIdentityDbContext 
{

    public EntAppDbContext(DbContextOptions<EntDbContext> options) : base(options)
    {
        
    }
    
   
}