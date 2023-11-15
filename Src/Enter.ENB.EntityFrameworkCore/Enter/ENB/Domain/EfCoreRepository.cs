using System.Linq.Expressions;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Domain;

[ExposeServices()]
public class EfCoreRepository<TDbContext, TEntity> :RepositoryBase<TEntity>,
    IRepository<TEntity>,
    IEfCoreRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : class, IEntEntity
{

    private readonly TDbContext _dbContext;

    public IEfCoreBulkOperationProvider? BulkOperationProvider => LazyServiceProvider.LazyGetService<IEfCoreBulkOperationProvider>();
    
    public EfCoreRepository(TDbContext dbContext,IEntLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
        _dbContext = dbContext;
        
    }

    public override async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        CheckAndSetId(entity);
        
        var savedEntity = (await _dbContext.Set<TEntity>().AddAsync(entity, GetCancellationToken(cancellationToken))).Entity;

        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
        }

        return savedEntity;
    }

    public async override Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        var entityArray = entities.ToArray();
        if (entityArray.IsNullOrEmpty())
        {
            return;
        }

        cancellationToken = GetCancellationToken(cancellationToken);

        foreach (var entity in entityArray)
        {
            CheckAndSetId(entity);
        }

        if (BulkOperationProvider != null)
        {
            await BulkOperationProvider.InsertManyAsync<TDbContext, TEntity>(
                this,
                entityArray,
                autoSave,
                GetCancellationToken(cancellationToken)
            );
            return;
        }

        await _dbContext.Set<TEntity>().AddRangeAsync(entityArray, cancellationToken);

        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async override Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {

        if (_dbContext.Set<TEntity>().Local.All(e => e != entity))
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Update(entity);
        }

        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
        }

        return entity;
    }

    public async override Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        var entityArray = entities.ToArray();
        if (entityArray.IsNullOrEmpty())
        {
            return;
        }

        cancellationToken = GetCancellationToken(cancellationToken);

        if (BulkOperationProvider != null)
        {
            await BulkOperationProvider.UpdateManyAsync<TDbContext, TEntity>(
                this,
                entityArray,
                autoSave,
                GetCancellationToken(cancellationToken)
                );

            return;
        }


        _dbContext.Set<TEntity>().UpdateRange(entityArray);

        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async override Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {

        _dbContext.Set<TEntity>().Remove(entity);

        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
        }
    }

    public async override Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        var entityArray = entities.ToArray();
        if (entityArray.IsNullOrEmpty())
        {
            return;
        }

        cancellationToken = GetCancellationToken(cancellationToken);

        if (BulkOperationProvider != null)
        {
            await BulkOperationProvider.DeleteManyAsync<TDbContext, TEntity>(
                this,
                entityArray,
                autoSave,
                cancellationToken
            );

            return;
        }


        _dbContext.RemoveRange(entityArray.Select(x => x));

        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async override Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        return includeDetails
            ? await (await WithDetailsAsync()).ToListAsync(GetCancellationToken(cancellationToken))
            : await (await GetQueryableAsync()).ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async override Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        return includeDetails
            ? await (await WithDetailsAsync()).Where(predicate).ToListAsync(GetCancellationToken(cancellationToken))
            : await (await GetQueryableAsync()).Where(predicate).ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async override Task<long> GetCountAsync(CancellationToken cancellationToken = default)
    {
        return await (await GetQueryableAsync()).LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public async override Task<List<TEntity>> GetPagedListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var queryable = includeDetails
            ? await WithDetailsAsync()
            : await GetQueryableAsync();

        return await queryable
            .OrderByIf<TEntity, IQueryable<TEntity>>(!string.IsNullOrWhiteSpace(sorting), sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async override Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        return (await GetDbSetAsync()).AsQueryable().AsNoTrackingIf(!ShouldTrackingEntityChange());
    }

    protected async override Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async override Task<TEntity?> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        return includeDetails
            ? await (await WithDetailsAsync())
                .Where(predicate)
                .SingleOrDefaultAsync(GetCancellationToken(cancellationToken))
            : await (await GetQueryableAsync())
                .Where(predicate)
                .SingleOrDefaultAsync(GetCancellationToken(cancellationToken));
    }

    public async override Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        var dbSet = _dbContext.Set<TEntity>();

        var entities = await dbSet
            .Where(predicate)
            .ToListAsync(GetCancellationToken(cancellationToken));

        await DeleteManyAsync(entities, autoSave, cancellationToken);

        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
        }
    }

    public async override Task DeleteDirectAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var dbSet = _dbContext.Set<TEntity>();
        await dbSet.Where(predicate).ExecuteDeleteAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task EnsureCollectionLoadedAsync<TProperty>(
        TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression,
        CancellationToken cancellationToken = default)
        where TProperty : class
    {
        await _dbContext
            .Entry(entity)
            .Collection(propertyExpression)
            .LoadAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task EnsurePropertyLoadedAsync<TProperty>(
        TEntity entity,
        Expression<Func<TEntity, TProperty?>> propertyExpression,
        CancellationToken cancellationToken = default)
        where TProperty : class
    {
        await _dbContext
            .Entry(entity)
            .Reference(propertyExpression)
            .LoadAsync(GetCancellationToken(cancellationToken));
    }

    public async override Task<IQueryable<TEntity>> WithDetailsAsync()
    {
            return await base.WithDetailsAsync();
    }

    public async override Task<IQueryable<TEntity>> WithDetailsAsync(params Expression<Func<TEntity, object>>[] propertySelectors)
    {
        return IncludeDetails(
            await GetQueryableAsync(),
            propertySelectors
        );
    }
    
    
    private static IQueryable<TEntity> IncludeDetails(
        IQueryable<TEntity> query,
        Expression<Func<TEntity, object>>[] propertySelectors)
    {
        if (!propertySelectors.IsNullOrEmpty())
        {
            foreach (var propertySelector in propertySelectors)
            {
                query = query.Include(propertySelector);
            }
        }

        return query;
    }

    protected virtual void CheckAndSetId(TEntity entity)
    {
        if (entity is IEntEntity<Guid> entityWithGuidId)
        {
            TrySetGuidId(entityWithGuidId);
        }
    }

    protected virtual void TrySetGuidId(IEntEntity<Guid> entity)
    {
        if (entity.Id != default)
        {
            return;
        }

        EntityHelper.TrySetId(
            entity,
            Guid.NewGuid, 
            true
        );
    }

    public Task<DbSet<TEntity>> GetDbSetAsync()
    {
        return Task.FromResult(_dbContext.Set<TEntity>());
    }
}

public class EfCoreRepository<TDbContext, TEntity, TKey> : EfCoreRepository<TDbContext, TEntity>,
    IEfCoreRepository<TEntity, TKey>,
    ISupportsExplicitLoading<TEntity>

    where TDbContext : DbContext
    where TEntity : class, IEntEntity<TKey>
{
    public EfCoreRepository(TDbContext dbContext,IEntLazyServiceProvider lazyServiceProvider)
        : base(dbContext,lazyServiceProvider)
    {

    }

    public virtual async Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        var entity = await FindAsync(id, includeDetails, GetCancellationToken(cancellationToken));

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity), id);
        }

        return entity;
    }

    public virtual async Task<TEntity?> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        return includeDetails
            ? await (await WithDetailsAsync()).OrderBy(e => e.Id).FirstOrDefaultAsync(e => e.Id!.Equals(id), GetCancellationToken(cancellationToken))
            : !ShouldTrackingEntityChange()
                ? await (await GetQueryableAsync()).OrderBy(e => e.Id).FirstOrDefaultAsync(e => e.Id!.Equals(id), GetCancellationToken(cancellationToken))
                : await (await GetDbSetAsync()).FindAsync(new object[] {id!}, GetCancellationToken(cancellationToken));
    }

    public virtual async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        var entity = await FindAsync(id, cancellationToken: cancellationToken);
        if (entity == null)
        {
            return;
        }

        await DeleteAsync(entity, autoSave, cancellationToken);
    }

    public virtual async Task DeleteManyAsync(IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        var entities = await (await GetDbSetAsync()).Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);

        await DeleteManyAsync(entities, autoSave, cancellationToken);
    }
}


