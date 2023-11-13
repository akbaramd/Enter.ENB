using Enter.ENB.Ddd.Application;
using Enter.ENB.Example.Domain;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Example.Application;

[DependsOnModules(
    typeof(EntExampleDomainModule),
    typeof(EntDddApplicationModule)
    )]
public class EntExampleApplicationModule :EntModule 
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new ApplicationConventionalRegistrar());
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        
        Configure<ApplicationOptions>("ApplicationException",options =>
        {
            if (options.UseAutoMapper)
            {
                context.Services.AddSingleton(null);
            }
        });
    }
}