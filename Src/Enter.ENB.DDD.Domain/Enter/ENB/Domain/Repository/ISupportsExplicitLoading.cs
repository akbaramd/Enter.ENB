using System.Linq.Expressions;
using Enter.ENB.Domain.Entities;

namespace Enter.ENB.Domain.Repository;

public interface ISupportsExplicitLoading<TEntity>
    where TEntity : class, IEntEntity
{
    Task EnsureCollectionLoadedAsync<TProperty>(
        TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression,
        CancellationToken cancellationToken)
        where TProperty : class;

    Task EnsurePropertyLoadedAsync<TProperty>(
        TEntity entity,
        Expression<Func<TEntity, TProperty?>> propertyExpression,
        CancellationToken cancellationToken)
        where TProperty : class;
}
