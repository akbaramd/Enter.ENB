using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain;
using Enter.ENB.Identity.Domain;

namespace Enter.ENB.Identity.EntityFrameworkCore.Repositories;


[ExposeServices(
    typeof(IEntUserRepository) )]
public class EntUserRepository :EfCoreRepository<EntIdentityDbContext,EntUser,Guid>, IEntUserRepository ,ITransientDependency  
{
    public EntUserRepository(EntIdentityDbContext dbContext,IEntLazyServiceProvider serviceProvider) : base(dbContext,serviceProvider)
    {
    }
}