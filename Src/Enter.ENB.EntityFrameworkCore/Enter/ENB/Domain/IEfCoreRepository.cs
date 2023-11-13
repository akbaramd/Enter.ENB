using Enter.ENB.Domain.Entities;
using Enter.ENB.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Domain;

public interface IEfCoreRepository<TEntity,TKey> : IRepository<TEntity,TKey>
    where TEntity : class, IEntEntity<TKey>
{
    Task<DbContext> GetDbContextAsync();

    Task<DbSet<TEntity>> GetDbSetAsync();
}

