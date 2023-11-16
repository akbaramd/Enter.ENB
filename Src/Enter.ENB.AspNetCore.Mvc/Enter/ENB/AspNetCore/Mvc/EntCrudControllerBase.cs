using Enter.ENB.Ddd.Application.Dtos;
using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Enter.ENB.AspNetCore.Mvc;

public class EntCrudControllerBase<TEntityDto,TKey,TQueryDto,TCreateDto,TUpdateDto> : EntControllerBase
{

    
    public EntCrudControllerBase(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
    }

    public ICrudAppService<TEntityDto, TKey, TQueryDto, TCreateDto, TUpdateDto> CrudService 
        => LazyServiceProvider.GetRequiredService<ICrudAppService<TEntityDto, TKey, TQueryDto, TCreateDto, TUpdateDto>>();


    [HttpGet("Get")]
    public async Task<PagedResultDto<TEntityDto>> GetListAsync([FromQuery] TQueryDto input)
    {
        return await CrudService.GetListAsync(input);
    }
    [HttpGet("Get/{id}")]
    public async Task<TEntityDto> GetAsync(TKey id)
    {
        return await CrudService.GetAsync(id);
    }
    
    [HttpPost("Create")]
    public async Task<TEntityDto> Create([FromBody] TCreateDto input)
    {
        return await CrudService.CreateAsync(input);
    }
    
    [HttpPut("Update/{id}")]
    public async Task<TEntityDto> Update(TKey id, [FromBody] TUpdateDto input)
    {
        return await CrudService.UpdateAsync(id,input);
    }
    
    [HttpDelete("Delete/{id}")]
    public async Task Delete(TKey id)
    {
         await CrudService.DeleteAsync(id);
    }

   
}

public class EntCrudControllerBase<TEntityDto,TKey,TQueryDto,TCreateUpdateDto> : EntCrudControllerBase<TEntityDto,TKey,TQueryDto,TCreateUpdateDto,TCreateUpdateDto>
{
    public EntCrudControllerBase(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
    }
}