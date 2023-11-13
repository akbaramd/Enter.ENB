using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Repository;
using Enter.ENB.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Identity.EntityFrameworkCore;

public class EntIdentityEntityFrameworkConventionalRegistrar : DefaultConventionalRegistrar
{
    
    protected override bool IsConventionalRegistrationDisabled(Type type)
    {
        return ! ReflectionHelper.IsAssignableToGenericType(type,typeof(IRepository<,>));
    }

    protected override ServiceLifetime? GetDefaultLifeTimeOrNull(Type type)
    {
        return ServiceLifetime.Transient;
    }
}