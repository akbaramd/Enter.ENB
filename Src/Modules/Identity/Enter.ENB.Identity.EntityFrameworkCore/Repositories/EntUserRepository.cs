using Enter.ENB.Domain;
using Enter.ENB.Identity.Domain;

namespace Enter.ENB.Identity.EntityFrameworkCore.Repositories;

public class EntUserRepository :EfCoreRepository<EntIdentityDbContext,EntUser,Guid>, IEntUserRepository
{
    public EntUserRepository(EntIdentityDbContext dbContext) : base(dbContext)
    {
        Console.WriteLine("ss");
    }
}