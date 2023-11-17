namespace Enter.ENB;

public interface IEntApplicationWithExternalServiceProvider : IEntApplication
{
    /// <summary>
    /// Creates the service provider, but not initializes the modules.
    /// Multiple calls returns the same service provider without creating again.
    /// </summary>
    void SetServiceProvider(IServiceProvider serviceProvider);

    /// <summary>
    /// Creates the service provider and initializes all the modules.
    /// If <see cref="CreateServiceProvider"/> method was called before,
    /// it does not re-create it, but uses the previous one.
    /// </summary>
    Task InitializeAsync(IServiceProvider serviceProvider);

    /// <summary>
    /// Creates the service provider and initializes all the modules.
    /// If <see cref="CreateServiceProvider"/> method was called before,
    /// it does not re-create it, but uses the previous one.
    /// </summary>
    void Initialize(IServiceProvider serviceProvider);
}
