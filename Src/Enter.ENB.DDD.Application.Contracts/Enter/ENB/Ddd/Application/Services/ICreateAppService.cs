namespace Enter.ENB.Ddd.Application.Services;

public interface ICreateApplicationService<TEntityDto>
    : ICreateApplicationService<TEntityDto, TEntityDto>
{

}

public interface ICreateApplicationService<TGetOutputDto, in TCreateInput>
    : IApplicationService
{
    Task<TGetOutputDto> CreateAsync(TCreateInput input);
}