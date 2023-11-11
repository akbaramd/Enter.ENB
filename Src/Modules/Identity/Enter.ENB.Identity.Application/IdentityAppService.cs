using Enter.ENB.DDD.Application.Dtos;
using Enter.ENB.DDD.Application.Services;
using Enter.ENB.Domain.Entities;
using Enter.ENB.Domain.Repository;

namespace Enter.ENB.Identity.Application;

public class IdentityAppService<TEntity,TKey> : ICrudAppService<TEntity,TKey> where TEntity : class, IEntEntity<TKey>
{
    public IdentityAppService(IRepository<TEntity, TKey> repository)
    {
        _repository = repository;
    }

    private readonly IRepository<TEntity, TKey> _repository;
    
    public async Task<TEntity> GetAsync(TKey id)
    {
        var find = await _repository.GetAsync(id);
        return find;
    }

    public async Task<PagedResultDto<TEntity>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var totalCount = await _repository.GetCountAsync();
        var query = await _repository.GetQueryableAsync();
        var res = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        return new PagedResultDto<TEntity>(totalCount,res);
    }

    public Task<TEntity> CreateAsync(TEntity input)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(TKey id, TEntity input)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TKey id)
    {
        throw new NotImplementedException();
    }
}