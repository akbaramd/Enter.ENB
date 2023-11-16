using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Identity.EntityFrameworkCore;

public interface IEntIdentityDbContext : IEntDbContext
{
    DbSet<EntIdentityUser> Users { get; }
    DbSet<EntIdentityRole> Roles { get; }
}