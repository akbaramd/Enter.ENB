namespace Enter.ENB.Modularity;

public interface IOnPostApplicationInitialization
{
    Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context);

    void OnPostApplicationInitialization(ApplicationInitializationContext context);
}
