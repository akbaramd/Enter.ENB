using Enter.ENB.Domain.Entities;
using Enter.ENB.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Domain;

public interface IEfCoreRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntEntity
{
    Task<DbSet<TEntity>> GetDbSetAsync();
}

public interface IEfCoreRepository<TEntity, TKey> : IEfCoreRepository<TEntity>, IRepository<TEntity, TKey>
    where TEntity : class, IEntEntity<TKey>
{

}
