using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Enter.ENB.AspNetCore.Mvc;

public class EntReadOnlyControllerBase<TEntityDto,TKey> : EntControllerBase
{

    
    public EntReadOnlyControllerBase(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
    }

    public IReadOnlyAppService<TEntityDto, TKey> CrudService 
        => LazyServiceProvider.GetRequiredService<IReadOnlyAppService<TEntityDto, TKey>>();


    [HttpGet("Get")]
    public async Task<PagedResultDto<TEntityDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
    {
        return await CrudService.GetListAsync(input);
    }
    [HttpGet("Get/{id}")]
    public async Task<TEntityDto> GetAsync(TKey id)
    {
        return await CrudService.GetAsync(id);
    }
    
    
   
}
