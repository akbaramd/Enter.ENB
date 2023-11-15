using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain;
using Enter.ENB.Domain.Repository;
using Enter.ENB.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Identity.EntityFrameworkCore.Repositories;


[ExposeServices(
    typeof(IEntUserRepository) )]
public class EntUserRepository :EfCoreRepository<EntIdentityDbContext,EntUser,Guid>, IEntUserRepository ,ITransientDependency  
{
    public EntUserRepository(EntIdentityDbContext dbContext) : base(dbContext)
    {
    }
}