using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Modularity;

public abstract class EntModule : IEntModule, IOnPreApplicationInitialization,
    IOnApplicationInitialization,
    IOnPostApplicationInitialization,
    IOnApplicationShutdown,
    IPreConfigureServices,
    IPostConfigureServices
{

    
    
    internal static void CheckEntModuleType(Type moduleType)
    {
        if (!IsEntModule(moduleType))
        {
            throw new ArgumentException("Given type is not an ENT module: " + moduleType.AssemblyQualifiedName);
        }
    }
    
    public static bool IsEntModule(Type type)
    {
        var typeInfo = type.GetTypeInfo();

        return
            typeInfo.IsClass &&
            !typeInfo.IsAbstract &&
       
            typeof(IEntModule).GetTypeInfo().IsAssignableFrom(type);
    }
    public IConfiguration Configuration { get; set; }
    public ServiceConfigurationContext ServiceConfigurationContext { get; set; }
   

    public virtual Task PreConfigureServicesAsync(ServiceConfigurationContext context)
    {
        PreConfigureServices(context);
        return Task.CompletedTask;
    }

    public virtual void PreConfigureServices(ServiceConfigurationContext context)
    {

    }

    public virtual Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        ConfigureServices(context);
        return Task.CompletedTask;
    }

    public virtual void ConfigureServices(ServiceConfigurationContext context)
    {

    }

    public virtual Task PostConfigureServicesAsync(ServiceConfigurationContext context)
    {
        PostConfigureServices(context);
        return Task.CompletedTask;
    }

    public virtual void PostConfigureServices(ServiceConfigurationContext context)
    {

    }

    public virtual Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        OnPreApplicationInitialization(context);
        return Task.CompletedTask;
    }

    public virtual void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {

    }

    public virtual Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        OnApplicationInitialization(context);
        return Task.CompletedTask;
    }

    public virtual void OnApplicationInitialization(ApplicationInitializationContext context)
    {

    }

    public virtual Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        OnPostApplicationInitialization(context);
        return Task.CompletedTask;
    }

    public virtual void OnPostApplicationInitialization(ApplicationInitializationContext context)
    {

    }

    public virtual Task OnApplicationShutdownAsync(ApplicationShutdownContext context)
    {
        OnApplicationShutdown(context);
        return Task.CompletedTask;
    }

    public virtual void OnApplicationShutdown(ApplicationShutdownContext context)
    {

    }
    
    
    
    protected void Configure<TOptions>(Action<TOptions> configureOptions)
        where TOptions : class
    {
        ServiceConfigurationContext.Services.Configure(configureOptions);
    }

    protected void Configure<TOptions>(string name, Action<TOptions> configureOptions)
        where TOptions : class
    {
        try
        {
            ServiceConfigurationContext.Services.Configure(name, configureOptions);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected void Configure<TOptions>(IConfiguration configuration)
        where TOptions : class
    {
        ServiceConfigurationContext.Services.Configure<TOptions>(configuration);
    }

    protected void Configure<TOptions>(IConfiguration configuration, Action<BinderOptions> configureBinder)
        where TOptions : class
    {
        ServiceConfigurationContext.Services.Configure<TOptions>(configuration, configureBinder);
    }

    protected void Configure<TOptions>(string name, IConfiguration configuration)
        where TOptions : class
    {
        ServiceConfigurationContext.Services.Configure<TOptions>(name, configuration);
    }

    protected void PreConfigure<TOptions>(Action<TOptions> configureOptions)
        where TOptions : class
    {
        // ServiceConfigurationContext.Services.PreConfigure(configureOptions);
    }

    protected void PostConfigure<TOptions>(Action<TOptions> configureOptions)
        where TOptions : class
    {
        ServiceConfigurationContext.Services.PostConfigure(configureOptions);
    }

    protected void PostConfigureAll<TOptions>(Action<TOptions> configureOptions)
        where TOptions : class
    {
        ServiceConfigurationContext.Services.PostConfigureAll(configureOptions);
    }
}