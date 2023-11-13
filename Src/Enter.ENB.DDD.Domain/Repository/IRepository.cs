using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Enter.ENB.Domain.Entities;

namespace Enter.ENB.Domain.Repository;



public interface IRepository<TEntity, TKey> 
    where TEntity : class, IEntEntity<TKey>
{
    Task InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    
    Task DeleteAsync(TEntity entity);
    Task DeleteAsync(TKey id);
}