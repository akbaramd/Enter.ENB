using System.Reflection;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Exceptions;
using Enter.ENB.Modularity;
using Enter.ENB.Reflection;
using Enter.ENB.Statics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Enter.ENB;

public class EntApplication : IEntApplication
{
    private bool _configuredServices;

    internal EntApplication(
        Type startupModuleType,
        IServiceCollection services, Action<EntApplicationCreationOptions>? optionsAction = null)
    {
        EntCheck.NotNull(startupModuleType, nameof(startupModuleType));
        EntCheck.NotNull(services, nameof(services));

        var options = new EntApplicationCreationOptions(services);
        optionsAction?.Invoke(options);

        AddCoreServices(services);

        services.TryAddObjectAccessor<IServiceProvider>();

        services.AddSingleton<IEntApplication>(this);
        services.AddSingleton<IApplicationInfoAccessor>(this);
        services.AddSingleton<IModuleContainer>(this);
        services.AddSingleton<IEntHostEnvironment>(new EntHostEnvironment()
        {
            EnvironmentName = options.Environment
        });
        
        services.TryAddSingleton<IModuleLoader>(new ModuleLoader());

        StartupModuleType = startupModuleType;
        Services = services;
        ApplicationName = GetApplicationName(options);
        Modules = LoadModules(services);

        var assemblyFinder = new AssemblyFinder(this);

        services.TryAddSingleton<IAssemblyFinder>(assemblyFinder);
        services.TryAddSingleton<ITypeFinder>(new TypeFinder(assemblyFinder));

        services.AddAssemblyOf<IEntApplication>();

        services.Configure<EntModuleLifecycleOptions>(lifecycleOptions =>
        {
            lifecycleOptions.Contributors.Add<OnPreApplicationInitializationModuleLifecycleContributor>();
            lifecycleOptions.Contributors.Add<OnApplicationInitializationModuleLifecycleContributor>();
            lifecycleOptions.Contributors.Add<OnPostApplicationInitializationModuleLifecycleContributor>();
            lifecycleOptions.Contributors.Add<OnApplicationShutdownModuleLifecycleContributor>();
        });

        if (!options.SkipConfigureServices) ConfigureServices();
    }


    public Type StartupModuleType { get; }

    public IServiceCollection Services { get; }
    public IServiceProvider ServiceProvider { get; private set; }
    public IReadOnlyCollection<IEntModuleDescriptor> Modules { get; }
    public string? ApplicationName { get; }
    public string InstanceId { get; } = Guid.NewGuid().ToString();

    public async Task ShutdownAsync()
    {
        using var scope = ServiceProvider.CreateScope();

        await scope.ServiceProvider
            .GetRequiredService<IModuleManager>()
            .ShutdownModulesAsync(new ApplicationShutdownContext(scope.ServiceProvider));
    }

    public void Shutdown()
    {
        using var scope = ServiceProvider.CreateScope();
        scope.ServiceProvider
            .GetRequiredService<IModuleManager>()
            .ShutdownModules(new ApplicationShutdownContext(scope.ServiceProvider));
    }

    public virtual void Dispose()
    {
    }


    public async Task ConfigureServicesAsync()
    {

        CheckMultipleConfigureServices();

        var context = new ServiceConfigurationContext(Services);
        Services.AddSingleton(context);

        foreach (var module in Modules)
            if (module.Instance is EntModule entModule)
            {
                entModule.ServiceConfigurationContext = context;
            }

        //PreConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPreConfigureServices))
            try
            {
                await ((IPreConfigureServices)module.Instance).PreConfigureServicesAsync(context);
            }
            catch (Exception ex)
            {
                throw new EntInitializationException(
                    $"An error occurred during {nameof(IPreConfigureServices.PreConfigureServicesAsync)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.",
                    ex);
            }

        var assemblies = new HashSet<Assembly>();

        //ConfigureServices
        foreach (var module in Modules)
        {
            if (module.Instance is EntModule { SkipAutoServiceRegistration: false })
                foreach (var assembly in module.AllAssemblies)
                {
                    if (assemblies.Contains(assembly)) continue;
                    Services.AddAssembly(assembly);
                    assemblies.Add(assembly);
                }

            try
            {
                await module.Instance.ConfigureServicesAsync(context);
            }
            catch (Exception ex)
            {
                throw new EntInitializationException(
                    $"An error occurred during {nameof(IEntModule.ConfigureServicesAsync)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.",
                    ex);
            }
        }

        //PostConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPostConfigureServices))
            try
            {
                await ((IPostConfigureServices)module.Instance).PostConfigureServicesAsync(context);
            }
            catch (Exception ex)
            {
                throw new EntInitializationException(
                    $"An error occurred during {nameof(IPostConfigureServices.PostConfigureServicesAsync)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.",
                    ex);
            }

        foreach (var module in Modules)
            if (module.Instance is EntModule entModule)
                entModule.ServiceConfigurationContext = null!;

        _configuredServices = true;

        TryToSetEnvironment(Services);

        // Logger.LogInformation("All Ent Modules Service ConfiguredAsync");
    }

    //TODO: We can extract a new class for this
    public void ConfigureServices()
    {
        // Logger.LogInformation("Start ConfiguredAsync All Module");

        CheckMultipleConfigureServices();

        var context = new ServiceConfigurationContext(Services);
        Services.AddSingleton(context);

        foreach (var module in Modules)
            if (module.Instance is EntModule entModule)
            {
                entModule.ServiceConfigurationContext = context;
            }

        //PreConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPreConfigureServices))
            try
            {
                ((IPreConfigureServices)module.Instance).PreConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new EntInitializationException(
                    $"An error occurred during {nameof(IPreConfigureServices.PreConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.",
                    ex);
            }

        var assemblies = new HashSet<Assembly>();

        //ConfigureServices
        foreach (var module in Modules)
        {
            if (module.Instance is EntModule { SkipAutoServiceRegistration: false })
                foreach (var assembly in module.AllAssemblies)
                    if (!assemblies.Contains(assembly))
                    {
                        Services.AddAssembly(assembly);
                        assemblies.Add(assembly);
                    }

            try
            {
                module.Instance.ConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new EntInitializationException(
                    $"An error occurred during {nameof(IEntModule.ConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.",
                    ex);
            }
        }

        //PostConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPostConfigureServices))
            try
            {
                ((IPostConfigureServices)module.Instance).PostConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new EntInitializationException(
                    $"An error occurred during {nameof(IPostConfigureServices.PostConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.",
                    ex);
            }

        foreach (var module in Modules)
            if (module.Instance is EntModule entModule)
                entModule.ServiceConfigurationContext = null!;

        _configuredServices = true;

        TryToSetEnvironment(Services);

        // Logger.LogInformation("All Ent Modules Service Configured. \n");
    }

    private static void AddCoreServices(IServiceCollection services)
    {
        services.AddOptions();
        services.AddLogging();
        services.AddLocalization();
    }


    protected void SetServiceProvider(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        ServiceProvider.GetRequiredService<ObjectAccessor<IServiceProvider>>().Value = ServiceProvider;
    }

    protected async Task InitializeModulesAsync()
    {
        using var scope = ServiceProvider.CreateScope();
        var manager = scope.ServiceProvider
            .GetRequiredService<IModuleManager>();

        await manager.InitializeModulesAsync(new ApplicationInitializationContext(scope.ServiceProvider));
    }

    protected void InitializeModules()
    {
        using var scope = ServiceProvider.CreateScope();
        // WriteInitLogs(scope.ServiceProvider);
        var manager = scope.ServiceProvider
            .GetRequiredService<IModuleManager>();

        manager.InitializeModules(new ApplicationInitializationContext(scope.ServiceProvider));
    }

    private IReadOnlyList<IEntModuleDescriptor> LoadModules(IServiceCollection services)
    {
        return services
            .GetSingletonInstance<IModuleLoader>()
            .LoadModules(
                services,
                StartupModuleType
            );
    }


    private static string? GetApplicationName(EntApplicationCreationOptions options)
    {
        if (!string.IsNullOrWhiteSpace(options.ApplicationName))
        {
            return options.ApplicationName!;
        }

        var configuration = options.Services.GetConfigurationOrNull();
        if (configuration != null)
        {
            var appNameConfig = configuration["ApplicationName"];
            if (!string.IsNullOrWhiteSpace(appNameConfig))
            {
                return appNameConfig!;
            }
        }

        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != null)
        {
            return entryAssembly.GetName().Name;
        }

        return null;
    }

    private void CheckMultipleConfigureServices()
    {
        if (_configuredServices)
            throw new EntInitializationException(
                "Services have already been configured! If you call ConfigureServicesAsync method, you must have set EntApplicationCreationOptions.SkipConfigureServices to true before.");
    }

    private static void TryToSetEnvironment(IServiceCollection services)
    {
        var abpHostEnvironment = services.GetSingletonInstance<IEntHostEnvironment>();
        if (abpHostEnvironment.EnvironmentName.IsNullOrWhiteSpace())
            abpHostEnvironment.EnvironmentName = Environments.Production;
    }
}