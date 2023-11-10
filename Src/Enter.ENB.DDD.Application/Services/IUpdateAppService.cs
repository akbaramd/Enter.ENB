namespace Enter.ENB.DDD.Application;

public interface IUpdateAppService<TEntityDto, in TKey>
    : IUpdateAppService<TEntityDto, TKey, TEntityDto>
{

}

public interface IUpdateAppService<TGetOutputDto, in TKey, in TUpdateInput>
    : IApplicationService
{
    Task<TGetOutputDto> UpdateAsync(TKey id, TUpdateInput input);
}