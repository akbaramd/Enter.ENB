using Enter.ENB.Domain.Entities;
using Enter.ENB.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Domain;

public static class EfCoreRepositoryExtensions
{

    public static Task<DbContext> GetDbContextAsync<TEntity>(this IReadOnlyBasicRepository<TEntity> repository)
        where TEntity : class, IEntEntity
    {
        return repository.ToEfCoreRepository().GetDbContextAsync();
    }


    public static Task<DbSet<TEntity>> GetDbSetAsync<TEntity>(this IReadOnlyBasicRepository<TEntity> repository)
        where TEntity : class, IEntEntity
    {
        return repository.ToEfCoreRepository().GetDbSetAsync();
    }

    public static IEfCoreRepository<TEntity> ToEfCoreRepository<TEntity>(this IReadOnlyBasicRepository<TEntity> repository)
        where TEntity : class, IEntEntity
    {
        if (repository is IEfCoreRepository<TEntity> efCoreRepository)
        {
            return efCoreRepository;
        }

        throw new ArgumentException("Given repository does not implement " + typeof(IEfCoreRepository<TEntity>).AssemblyQualifiedName, nameof(repository));
    }

    public static IQueryable<TEntity> AsNoTrackingIf<TEntity>(this IQueryable<TEntity> queryable, bool condition)
        where TEntity : class, IEntEntity
    {
        return condition ? queryable.AsNoTracking() : queryable;
    }
}
