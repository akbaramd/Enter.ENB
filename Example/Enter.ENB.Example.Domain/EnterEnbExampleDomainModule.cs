using Enter.ENB.Domain;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Example.Domain;

[DependsOnModules(typeof(EnterEnbDddDomainModule))]
public class EnterEnbExampleDomainModule : EntModule 
{
   
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :PreConfigureServices");
    }
    
    public override Task PreConfigureServicesAsync(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :PreConfigureServicesAsync");
        return base.PreConfigureServicesAsync(context);
    }
    
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :ConfigureServices");
    }


    public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :ConfigureServicesAsync");
        return base.ConfigureServicesAsync(context);
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :PostConfigureServices");
        base.PostConfigureServices(context);
    }

    public override Task PostConfigureServicesAsync(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :PostConfigureServicesAsync");
        return base.PostConfigureServicesAsync(context);
    }


    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :OnPreApplicationInitialization");
        base.OnPreApplicationInitialization(context);
    }

    public override Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :OnPreApplicationInitializationAsync");
        return base.OnPreApplicationInitializationAsync(context);
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :OnApplicationInitialization");
        base.OnApplicationInitialization(context);
    }

    public override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    { 
        Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :OnApplicationInitializationAsync");
        return base.OnApplicationInitializationAsync(context);
    }

    public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
    {
         Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :OnPostApplicationInitialization");
        base.OnPostApplicationInitialization(context);
    }

    public override Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :OnPostApplicationInitializationAsync");
        return base.OnPostApplicationInitializationAsync(context);
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :OnApplicationShutdown");
        base.OnApplicationShutdown(context);
    }

    public override Task OnApplicationShutdownAsync(ApplicationShutdownContext context)
    {
        Console.WriteLine($"{nameof(EnterEnbExampleDomainModule)} :OnApplicationShutdownAsync");
       
        return base.OnApplicationShutdownAsync(context);
    }
}