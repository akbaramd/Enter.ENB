namespace Enter.ENB.DDD.Application.Services;

public interface IDeleteAppService<in TKey> : IApplicationService
{
    Task DeleteAsync(TKey id);
}