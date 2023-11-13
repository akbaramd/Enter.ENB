using System.Reflection;
using Enter.ENB.Exceptions;
using Enter.ENB.Modularity;
using Enter.ENB.Reflection;
using Enter.ENB.Statics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Enter.ENB;

public class EntApplicationBase : IEntApplication
{
    private bool _configuredServices;

    public IConfiguration Configuration { get; set; }
    internal EntApplicationBase(
        Type startupModuleType,
        IServiceCollection services)
    {
        EntCheck.NotNull(startupModuleType, nameof(startupModuleType));
        EntCheck.NotNull(services, nameof(services));

        StartupModuleType = startupModuleType;
        Services = services;
        Configuration = services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<IConfiguration>();
        services.TryAddObjectAccessor<IServiceProvider>();

        // var options = new EntApplicationCreationOptions(services);
        // optionsAction?.Invoke(options);
        
        ApplicationName = GetApplicationName();

        services.AddSingleton<IEntApplication>(this);
        services.AddSingleton<IApplicationInfoAccessor>(this);
        services.AddSingleton<IModuleContainer>(this);

        AddCoreServices(services);

        AddCoreEntServices(services, this);

        Modules = LoadModules(services);
    }

    public Type StartupModuleType { get; } = default!;

    public IServiceCollection Services { get; } = default!;
    public IServiceProvider ServiceProvider { get; set; } = default!;

    public IReadOnlyCollection<IEntModuleDescriptor> Modules { get; } = default!;
    public string? ApplicationName { get; }
    public string InstanceId { get; } = default!;

    public virtual async Task ShutdownAsync()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            await scope.ServiceProvider
                .GetRequiredService<IModuleManager>()
                .ShutdownModulesAsync(new ApplicationShutdownContext(scope.ServiceProvider));
        }
    }

    public virtual void Shutdown()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            scope.ServiceProvider
                .GetRequiredService<IModuleManager>()
                .ShutdownModules(new ApplicationShutdownContext(scope.ServiceProvider));
        }
    }

    public virtual void Dispose()
    {
    }


    public virtual async Task ConfigureServicesAsync()
    {
        CheckMultipleConfigureServices();

        
        
        var context = new ServiceConfigurationContext(Services);
        Services.AddSingleton(context);

        foreach (var module in Modules)
            if (module.Instance is EntModule entModule)
            {
                entModule.ServiceConfigurationContext = context;
                entModule.Configuration = Configuration;
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
            
            if (module.Instance is EntModule EntModule)
            {
                // if (!EntModule.SkipAutoServiceRegistration)
                // {
                    foreach (var assembly in module.AllAssemblies)
                    {
                        if (!assemblies.Contains(assembly))
                        {
                            Services.AddAssembly(assembly);
                            assemblies.Add(assembly);
                        }
                    }
                // }
            }

            try
            {
                await module.Instance.ConfigureServicesAsync(context);
            }
            catch (Exception ex)
            {
                throw new EntInitializationException($"An error occurred during {nameof(IEntModule.ConfigureServicesAsync)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
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

        // TryToSetEnvironment(Services);
    }

    //TODO: We can extract a new class for this
    public virtual void ConfigureServices()
    {
        CheckMultipleConfigureServices();

        var context = new ServiceConfigurationContext(Services);
        Services.AddSingleton(context);

        foreach (var module in Modules)
            if (module.Instance is EntModule entModule)
            {
                entModule.ServiceConfigurationContext = context;
                entModule.Configuration = Configuration;
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
            if (module.Instance is EntModule entModule)
            {
                // if (!entModule.SkipAutoServiceRegistration)
                // {
                foreach (var assembly in module.AllAssemblies)
                {
                    if (!assemblies.Contains(assembly))
                    {
                        Services.AddAssembly(assembly);
                        assemblies.Add(assembly);
                    }
                }
                // }
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

        //TryToSetEnvironment(Services);
    }

    internal static void AddCoreServices(IServiceCollection services)
    {
        services.AddOptions();
        services.AddLogging();
        services.AddLocalization();
    }

    internal void AddCoreEntServices(IServiceCollection services,
        IEntApplication entApplication)
    {
        var moduleLoader = new ModuleLoader();
        var assemblyFinder = new AssemblyFinder(entApplication);
        var typeFinder = new TypeFinder(assemblyFinder);

        
        services.TryAddSingleton<IModuleLoader>(moduleLoader);
        services.TryAddSingleton<IAssemblyFinder>(assemblyFinder);
        services.TryAddSingleton<ITypeFinder>(typeFinder);
        

        services.AddAssemblyOf<IEntApplication>();
        
        services.Configure<EntModuleLifecycleOptions>(options =>
        {
            options.Contributors.Add<OnPreApplicationInitializationModuleLifecycleContributor>();
            options.Contributors.Add<OnApplicationInitializationModuleLifecycleContributor>();
            options.Contributors.Add<OnPostApplicationInitializationModuleLifecycleContributor>();
            options.Contributors.Add<OnApplicationShutdownModuleLifecycleContributor>();
        });
    }

    protected virtual void SetServiceProvider(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    protected virtual async Task InitializeModulesAsync()
    {
        using var scope = ServiceProvider.CreateScope();
        var manager = scope.ServiceProvider
            .GetRequiredService<IModuleManager>();

        await manager.InitializeModulesAsync(new ApplicationInitializationContext(scope.ServiceProvider));
    }

    protected virtual void InitializeModules()
    {
        using var scope = ServiceProvider.CreateScope();
        // WriteInitLogs(scope.ServiceProvider);
        var manager = scope.ServiceProvider
            .GetRequiredService<IModuleManager>();

         manager.InitializeModules(new ApplicationInitializationContext(scope.ServiceProvider));
    }

    protected virtual IReadOnlyList<IEntModuleDescriptor> LoadModules(IServiceCollection services)
    {
        return services
            .GetSingletonInstance<IModuleLoader>()
            .LoadModules(
                services,
                StartupModuleType
            );
    }

    private static string? GetApplicationName()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != null) return entryAssembly.GetName().Name;

        return null;
    }

    private void CheckMultipleConfigureServices()
    {
        if (_configuredServices)
            throw new EntInitializationException(
                "Services have already been configured! If you call ConfigureServicesAsync method, you must have set EntApplicationCreationOptions.SkipConfigureServices to true before.");
    }
}