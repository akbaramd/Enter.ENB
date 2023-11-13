using Enter.ENB.EntityFrameworkCore;
using Enter.ENB.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Identity.EntityFrameworkCore;

public interface IEntIdentityDbContext : IEntDbContext
{
    DbSet<EntUser> Users { get; }
}