using Enter.ENB.Domain;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Example.Domain;

[DependsOnModules(
    typeof(EntDddDomainModule),
    typeof(EntIdentityDomainModule)
    )]
public class EntExampleDomainModule : EntModule 
{
   
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EntExampleDomainModule)} :PreConfigureServices");
    }
    
    public override Task PreConfigureServicesAsync(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EntExampleDomainModule)} :PreConfigureServicesAsync");
        return base.PreConfigureServicesAsync(context);
    }
    
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EntExampleDomainModule)} :ConfigureServices");
    }


    public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EntExampleDomainModule)} :ConfigureServicesAsync");
        return base.ConfigureServicesAsync(context);
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EntExampleDomainModule)} :PostConfigureServices");
        base.PostConfigureServices(context);
    }

    public override Task PostConfigureServicesAsync(ServiceConfigurationContext context)
    {
        Console.WriteLine($"{nameof(EntExampleDomainModule)} :PostConfigureServicesAsync");
        return base.PostConfigureServicesAsync(context);
    }


    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        Console.WriteLine($"{nameof(EntExampleDomainModule)} :OnPreApplicationInitialization");
        base.OnPreApplicationInitialization(context);
    }

    public override Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        Console.WriteLine($"{nameof(EntExampleDomainModule)} :OnPreApplicationInitializationAsync");
        return base.OnPreApplicationInitializationAsync(context);
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        Console.WriteLine($"{nameof(EntExampleDomainModule)} :OnApplicationInitialization");
        base.OnApplicationInitialization(context);
    }

    public override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    { 
        Console.WriteLine($"{nameof(EntExampleDomainModule)} :OnApplicationInitializationAsync");
        return base.OnApplicationInitializationAsync(context);
    }

    public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
    {
         Console.WriteLine($"{nameof(EntExampleDomainModule)} :OnPostApplicationInitialization");
        base.OnPostApplicationInitialization(context);
    }

    public override Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        Console.WriteLine($"{nameof(EntExampleDomainModule)} :OnPostApplicationInitializationAsync");
        return base.OnPostApplicationInitializationAsync(context);
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        Console.WriteLine($"{nameof(EntExampleDomainModule)} :OnApplicationShutdown");
        base.OnApplicationShutdown(context);
    }

    public override Task OnApplicationShutdownAsync(ApplicationShutdownContext context)
    {
        Console.WriteLine($"{nameof(EntExampleDomainModule)} :OnApplicationShutdownAsync");
       
        return base.OnApplicationShutdownAsync(context);
    }
}