namespace Enter.ENB.Ddd.Application.Services;

public interface IDeleteAppService<in TKey> : IApplicationService
{
    Task DeleteAsync(TKey id);
}