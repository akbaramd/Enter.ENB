using Enter.ENB.Modularity;

namespace Enter.ENB;

public interface IOnApplicationShutdown
{
    Task OnApplicationShutdownAsync(ApplicationShutdownContext context);

    void OnApplicationShutdown( ApplicationShutdownContext context);
}
