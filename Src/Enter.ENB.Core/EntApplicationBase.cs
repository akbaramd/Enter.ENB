using System.Reflection;
using Enter.ENB.Core.Exceptions;
using Enter.ENB.Core.Modularity;
using Enter.ENB.Core.Reflection;
using Enter.ENB.Extensions;
using Enter.ENB.Modularity;
using Enter.ENB.Statics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Modularity;

namespace Enter.ENB.Core;

public class EntApplicationBase : IEntApplication
{
    private bool _configuredServices;
    public Type StartupModuleType { get; } = default!;

    public IServiceCollection Services { get; } = default!;
    public IServiceProvider ServiceProvider { get; set; } = default!;

    public IReadOnlyCollection<IEntModuleDescriptor> Modules { get; } = default!;
    public string? ApplicationName { get; } = default!;
    public string ApplicationInstance { get; } = default!;


    internal EntApplicationBase(
        Type startupModuleType,
        IServiceCollection services)
    {
        EntCheck.NotNull(startupModuleType, nameof(startupModuleType));
        EntCheck.NotNull(services, nameof(services));

        StartupModuleType = startupModuleType;
        Services = services;

        // services.TryAddObjectAccessor<IServiceProvider>();

        ApplicationName = GetApplicationName();

        services.AddSingleton<IEntApplication>(this);
        services.AddSingleton<IApplicationInfoAccessor>(this);
        services.AddSingleton<IModuleContainer>(this);

        AddCoreServices(services);
        
        AddCoreAbpServices(services,this);

        Modules = LoadModules(services);

        ConfigureServices();
    }
    internal static void AddCoreServices( IServiceCollection services)
    {
        services.AddOptions();
        services.AddLogging();
        services.AddLocalization();
    }

    internal void AddCoreAbpServices(IServiceCollection services,
        IEntApplication abpApplication)
    {
        var moduleLoader = new ModuleLoader();
        var assemblyFinder = new AssemblyFinder(abpApplication);
        var typeFinder = new TypeFinder(assemblyFinder);


        services.TryAddSingleton<IModuleLoader>(moduleLoader);
        services.TryAddSingleton<IAssemblyFinder>(assemblyFinder);
        services.TryAddSingleton<ITypeFinder>(typeFinder);

        services.Configure<AbpModuleLifecycleOptions>(options =>
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
        // using (var scope = ServiceProvider.CreateScope())
        // {
        //     await scope.ServiceProvider
        //         .GetRequiredService<IModuleManager>()
        //         .InitializeModulesAsync(new ApplicationInitializationContext(scope.ServiceProvider));
        // }
    }

    protected virtual void InitializeModules()
    {
        // using (var scope = ServiceProvider.CreateScope())
        // {
        //     WriteInitLogs(scope.ServiceProvider);
        //     scope.ServiceProvider
        //         .GetRequiredService<IModuleManager>()
        //         .InitializeModules(new ApplicationInitializationContext(scope.ServiceProvider));
        // }
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
        if (entryAssembly != null)
        {
            return entryAssembly.GetName().Name;
        }

        return null;
    }
    public Task ShutdownAsync()
    {
        throw new NotImplementedException();
    }

    public virtual void Dispose()
    {
        throw new NotImplementedException();
    }
    private void CheckMultipleConfigureServices()
    {
        if (_configuredServices)
        {
            throw new EntException("Services have already been configured! If you call ConfigureServicesAsync method, you must have set AbpApplicationCreationOptions.SkipConfigureServices to true before.");
        }
    }

    //TODO: We can extract a new class for this
    public virtual void ConfigureServices()
    {
        CheckMultipleConfigureServices();

        var context = new ServiceConfigurationContext(Services);
        Services.AddSingleton(context);

        foreach (var module in Modules)
        {
            if (module.Instance is EntModule abpModule)
            {
                abpModule.ServiceConfigurationContext = context;
            }
        }

        //PreConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPreConfigureServices))
        {
            try
            {
                ((IPreConfigureServices)module.Instance).PreConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new EntException($"An error occurred during {nameof(IPreConfigureServices.PreConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }

        var assemblies = new HashSet<Assembly>();

        //ConfigureServices
        foreach (var module in Modules)
        {
            //if (module.Instance is EntModule abpModule)
            //{
                // if (!abpModule.SkipAutoServiceRegistration)
                // {
                //     foreach (var assembly in module.AllAssemblies)
                //     {
                //         if (!assemblies.Contains(assembly))
                //         {
                //             Services.AddAssembly(assembly);
                //             assemblies.Add(assembly);
                //         }
                //     }
                // }
            //}

            try
            {
                module.Instance.ConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new EntException($"An error occurred during {nameof(IEntModule.ConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }

        //PostConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPostConfigureServices))
        {
            try
            {
                ((IPostConfigureServices)module.Instance).PostConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new EntException($"An error occurred during {nameof(IPostConfigureServices.PostConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
            }
        }

        foreach (var module in Modules)
        {
            if (module.Instance is EntModule abpModule)
            {
                abpModule.ServiceConfigurationContext = null!;
            }
        }

        _configuredServices = true;

        // TryToSetEnvironment(Services);
    }

    public async Task Shutdown()
    {
    }
}