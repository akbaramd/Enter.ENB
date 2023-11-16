using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Identity.EntityFrameworkCore.Repositories;



[ExposeServices(typeof(IEntUserRepository))]
public class EntUserRepository :EfCoreRepository<EntIdentityDbContext,EntIdentityUser,Guid>, IEntUserRepository ,ITransientDependency
{
    private readonly EntIdentityDbContext _dbContext;

    public EntUserRepository(EntIdentityDbContext dbContext,IEntLazyServiceProvider serviceProvider) : base(dbContext,serviceProvider)
    {
        _dbContext = dbContext;
    }

    public Task<EntIdentityUser?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return _dbContext.Set<EntIdentityUser>().FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
}