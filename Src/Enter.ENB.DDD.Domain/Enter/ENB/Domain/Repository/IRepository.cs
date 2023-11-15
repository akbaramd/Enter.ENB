using System.Linq.Expressions;
using Enter.ENB.Domain.Entities;

namespace Enter.ENB.Domain.Repository;


public interface IRepository
{
    bool? IsChangeTrackingEnabled { get; }
}


public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
    where TEntity : class, IEntEntity
{
    Task<TEntity?> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDetails = true,
        CancellationToken cancellationToken = default
    );
    
    Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDetails = true,
        CancellationToken cancellationToken = default
    );
    
    Task DeleteAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool autoSave = false,
        CancellationToken cancellationToken = default
    );
    
    Task DeleteDirectAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default
    );
}

public interface IRepository<TEntity, TKey> : IRepository<TEntity>, IReadOnlyRepository<TEntity, TKey>, IBasicRepository<TEntity, TKey>
    where TEntity : class, IEntEntity<TKey>
{
}