using Enter.ENB.Example.Application;
using Enter.ENB.Modularity;

namespace Enter.ENB.Example.Api;

[DependsOnModules(typeof(EnterEnbExampleApplicationModule))]
public class EnterEnbExampleApiModule : EntModule
{
    
   
    
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :PreConfigureServices");
    }
    
    public override Task PreConfigureServicesAsync(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :PreConfigureServicesAsync");
        return base.PreConfigureServicesAsync(context);
    }
    
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :ConfigureServices");
    }


    public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :ConfigureServicesAsync");
        return base.ConfigureServicesAsync(context);
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :PostConfigureServices");
        base.PostConfigureServices(context);
    }

    public override Task PostConfigureServicesAsync(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :PostConfigureServicesAsync");
        return base.PostConfigureServicesAsync(context);
    }


    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :OnPreApplicationInitialization");
        base.OnPreApplicationInitialization(context);
    }

    public override Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :OnPreApplicationInitializationAsync");
        return base.OnPreApplicationInitializationAsync(context);
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :OnApplicationInitialization");
        base.OnApplicationInitialization(context);
    }

    public override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    { 
        Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :OnApplicationInitializationAsync");
        return base.OnApplicationInitializationAsync(context);
    }

    public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
    {
         Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :OnPostApplicationInitialization");
        base.OnPostApplicationInitialization(context);
    }

    public override Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :OnPostApplicationInitializationAsync");
        return base.OnPostApplicationInitializationAsync(context);
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :OnApplicationShutdown");
        base.OnApplicationShutdown(context);
    }

    public override Task OnApplicationShutdownAsync(ApplicationShutdownContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleApiModule)} :OnApplicationShutdownAsync");
       
        return base.OnApplicationShutdownAsync(context);
    }
}