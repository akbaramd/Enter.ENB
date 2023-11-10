namespace Enter.ENB.DDD.Application;

public interface ICreateAppService<TEntityDto>
    : ICreateAppService<TEntityDto, TEntityDto>
{

}

public interface ICreateAppService<TGetOutputDto, in TCreateInput>
    : IApplicationService
{
    Task<TGetOutputDto> CreateAsync(TCreateInput input);
}