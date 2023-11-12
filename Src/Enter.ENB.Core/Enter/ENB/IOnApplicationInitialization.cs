using Enter.ENB.Modularity;

namespace Enter.ENB;

public interface IOnApplicationInitialization
{
    Task OnApplicationInitializationAsync(ApplicationInitializationContext context);

    void OnApplicationInitialization( ApplicationInitializationContext context);
}
