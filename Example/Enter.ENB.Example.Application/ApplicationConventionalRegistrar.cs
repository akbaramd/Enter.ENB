using Enter.ENB.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Example.Application;

public class ApplicationConventionalRegistrar : DefaultConventionalRegistrar
{
    
    

    protected override bool IsConventionalRegistrationDisabled(Type type)
    {
        return type.Name != nameof(ApplicationOptions);
    }

    protected override ServiceLifetime? GetDefaultLifeTimeOrNull(Type type)
    {
        return ServiceLifetime.Transient;
    }
}