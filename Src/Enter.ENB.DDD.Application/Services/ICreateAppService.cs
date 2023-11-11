namespace Enter.ENB.DDD.Application.Services;

public interface ICreateAppService<TEntityDto>
    : ICreateAppService<TEntityDto, TEntityDto>
{

}

public interface ICreateAppService<TGetOutputDto, in TCreateInput>
    : IAppService
{
    Task<TGetOutputDto> CreateAsync(TCreateInput input);
}