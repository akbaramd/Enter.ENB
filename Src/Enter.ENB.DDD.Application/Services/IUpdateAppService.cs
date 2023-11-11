namespace Enter.ENB.DDD.Application.Services;

public interface IUpdateAppService<TEntityDto, in TKey>
    : IUpdateAppService<TEntityDto, TKey, TEntityDto>
{

}

public interface IUpdateAppService<TGetOutputDto, in TKey, in TUpdateInput>
    : IAppService
{
    Task<TGetOutputDto> UpdateAsync(TKey id, TUpdateInput input);
}