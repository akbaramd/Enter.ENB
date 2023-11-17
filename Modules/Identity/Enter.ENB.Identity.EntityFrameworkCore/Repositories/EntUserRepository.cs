using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Identity.EntityFrameworkCore.Repositories;



[ExposeServices(typeof(IEntIdentityUserRepository))]
public class EntIdentityUserRepository :EfCoreRepository<EntIdentityDbContext,EntIdentityUser,Guid>, IEntIdentityUserRepository ,ITransientDependency
{
    private readonly EntIdentityDbContext _dbContext;

    public EntIdentityUserRepository(EntIdentityDbContext dbContext,IEntLazyServiceProvider serviceProvider) : base(dbContext,serviceProvider)
    {
        _dbContext = dbContext;
    }

    public Task<EntIdentityUser?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return _dbContext.Set<EntIdentityUser>().FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
    
    public override async Task<IQueryable<EntIdentityUser>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
    
    
}