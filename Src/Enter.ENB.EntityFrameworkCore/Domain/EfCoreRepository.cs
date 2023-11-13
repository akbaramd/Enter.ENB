using System.Data;
using System.Linq.Expressions;
using Enter.ENB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Enter.ENB.EntityFrameworkCore.Domain;

public class EfCoreRepository<TDbContext, TEntity,TKey> :  IEfCoreRepository<TEntity,TKey>
    where TDbContext : IEntDbContext
    where TEntity : class, IEntEntity<TKey>
{

    private readonly TDbContext _dbContext;
    
    public Task InsertAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TKey id)
    {
        throw new NotImplementedException();
    }

    public Task<DbContext> GetDbContextAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DbSet<TEntity>> GetDbSetAsync()
    {
        throw new NotImplementedException();
    }
}