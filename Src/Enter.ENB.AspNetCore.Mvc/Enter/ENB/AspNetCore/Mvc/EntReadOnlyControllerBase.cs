using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Enter.ENB.AspNetCore.Mvc;

public class EntReadOnlyControllerBase<TEntityDto,TKey,TQuery> : EntControllerBase
{
    public EntReadOnlyControllerBase(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
    }

    public IReadOnlyAppService<TEntityDto, TKey,TQuery> CrudService 
        => LazyServiceProvider.GetRequiredService<IReadOnlyAppService<TEntityDto, TKey,TQuery>>();


    [HttpGet("Get")]
    public async Task<PagedResultDto<TEntityDto>> GetListAsync([FromQuery] TQuery input)
    {
        return await CrudService.GetListAsync(input);
    }
    [HttpGet("Get/{id}")]
    public async Task<TEntityDto> GetAsync(TKey id)
    {
        return await CrudService.GetAsync(id);
    }
    
    
   
}
