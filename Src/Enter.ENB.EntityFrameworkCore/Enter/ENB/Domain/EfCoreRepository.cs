using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Domain.Repository;
using Enter.ENB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Domain;

[ExposeServices()]
public class EfCoreRepository<TDbContext, TEntity, TKey> :
    IRepository<TEntity,TKey>,
    IEfCoreRepository<TEntity, TKey>
    where TDbContext : DbContext
    where TEntity : class, IEntEntity<TKey>
{
    private readonly TDbContext _dbContext;


    public EfCoreRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InsertAsync(TEntity entity)
    {
        var dbSet = await GetDbSetAsync();
        await dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var dbSet = await GetDbSetAsync();
        dbSet.Update(entity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        var dbSet = await GetDbSetAsync();
        dbSet.Remove(entity);
    }

    public async Task DeleteAsync(TKey id)
    {
        var dbSet = await GetDbSetAsync();
        var find = await Find(id);
        if (find != null) dbSet.Remove(find);
    }

    public Task<DbContext> GetDbContextAsync()
    {
        return Task.FromResult<DbContext>(_dbContext);
    }

    public Task<DbSet<TEntity>> GetDbSetAsync()
    {
        return Task.FromResult(_dbContext.Set<TEntity>());
    }

    public async Task<TEntity?> Find(TKey id)
    {
        var dbSet = await GetDbSetAsync();
        var find = await dbSet.FirstOrDefaultAsync(x => x.Id != null && x.Id.Equals(id));
        return find;
    }

    public Task<TEntity> FindAll()
    {
        throw new NotImplementedException();
    }
}