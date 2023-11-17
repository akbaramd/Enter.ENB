namespace Enter.ENB;

public interface IEntApplicationWithInternalServiceProvider : IEntApplication
{
    
    IServiceProvider CreateServiceProvider();

    Task InitializeAsync();

    void Initialize();
}
