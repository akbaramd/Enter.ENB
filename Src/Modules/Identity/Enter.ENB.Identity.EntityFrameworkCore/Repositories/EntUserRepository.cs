using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain;
using Enter.ENB.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Identity.EntityFrameworkCore.Repositories;


public class EntUserRepository<TDbContent> :EfCoreRepository<TDbContent,EntUser,Guid>, IEntUserRepository where TDbContent : DbContext 
{
    public EntUserRepository(TDbContent dbContext) : base(dbContext)
    {
        Console.WriteLine("ss");
    }
}