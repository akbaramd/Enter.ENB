using Enter.ENB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Domain;

public interface IEfCoreBulkOperationProvider
{
    Task InsertManyAsync<TDbContext, TEntity>(
        IEfCoreRepository<TEntity> repository,
        IEnumerable<TEntity> entities,
        bool autoSave,
        CancellationToken cancellationToken
    )
        where TDbContext : DbContext
        where TEntity : class, IEntEntity;


    Task UpdateManyAsync<TDbContext, TEntity>(
        IEfCoreRepository<TEntity> repository,
        IEnumerable<TEntity> entities,
        bool autoSave,
        CancellationToken cancellationToken
    )
        where TDbContext : DbContext
        where TEntity : class, IEntEntity;


    Task DeleteManyAsync<TDbContext, TEntity>(
        IEfCoreRepository<TEntity> repository,
        IEnumerable<TEntity> entities,
        bool autoSave,
        CancellationToken cancellationToken
    )
        where TDbContext : DbContext
        where TEntity : class, IEntEntity;
}