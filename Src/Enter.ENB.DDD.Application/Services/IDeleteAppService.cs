namespace Enter.ENB.DDD.Application;

public interface IDeleteAppService<in TKey> : IApplicationService
{
    Task DeleteAsync(TKey id);
}