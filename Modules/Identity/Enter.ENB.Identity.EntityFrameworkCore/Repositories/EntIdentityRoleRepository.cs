using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Identity.EntityFrameworkCore.Repositories;



[ExposeServices(typeof(IEntIdentityRoleRepository))]
public class EntIdentityRoleRepository :EfCoreRepository<EntIdentityDbContext,EntIdentityRole,Guid>, IEntIdentityRoleRepository ,ITransientDependency 
{
    private readonly EntIdentityDbContext _dbContext;

    public EntIdentityRoleRepository(EntIdentityDbContext dbContext,IEntLazyServiceProvider serviceProvider) : base(dbContext,serviceProvider)
    {
        _dbContext = dbContext;
    }

    
}