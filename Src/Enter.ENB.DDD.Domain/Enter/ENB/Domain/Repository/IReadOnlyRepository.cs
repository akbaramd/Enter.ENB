using System.Linq.Expressions;
using Enter.ENB.Domain.Entities;
using Enter.Enb.Linq;

namespace Enter.ENB.Domain.Repository;


public interface IReadOnlyRepository<TEntity> : IReadOnlyBasicRepository<TEntity>
     where TEntity : class, IEntEntity
{
     IAsyncQueryableExecuter AsyncExecuter { get; }

     Task<IQueryable<TEntity>> WithDetailsAsync(); //TODO: CancellationToken

     Task<IQueryable<TEntity>> WithDetailsAsync(params Expression<Func<TEntity, object>>[] propertySelectors); //TODO: CancellationToken

     Task<IQueryable<TEntity>> GetQueryableAsync(); //TODO: CancellationToken

     /// <summary>
     /// Gets a list of entities by the given <paramref name="predicate"/>.
     /// </summary>
     /// <param name="predicate">A condition to filter the entities</param>
     /// <param name="includeDetails">Set true to include details (sub-collections) of this entity</param>
     /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
     Task<List<TEntity>> GetListAsync(
          Expression<Func<TEntity, bool>> predicate,
          bool includeDetails = false,
          CancellationToken cancellationToken = default);
}

public interface IReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity>, IReadOnlyBasicRepository<TEntity, TKey>
     where TEntity : class, IEntEntity<TKey>
{

}