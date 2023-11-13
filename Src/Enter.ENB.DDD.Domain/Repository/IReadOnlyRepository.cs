using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Enter.ENB.Domain.Entities;

namespace Enter.ENB.Domain.Repository;


public interface IReadOnlyRepository<TEntity, TKey> where TEntity : IEntEntity
{
     Task<TEntity> Find(TKey id);
     Task<TEntity> FindAll();
     
}