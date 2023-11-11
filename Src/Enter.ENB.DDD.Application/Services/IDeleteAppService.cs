namespace Enter.ENB.DDD.Application.Services;

public interface IDeleteAppService<in TKey> : IAppService
{
    Task DeleteAsync(TKey id);
}