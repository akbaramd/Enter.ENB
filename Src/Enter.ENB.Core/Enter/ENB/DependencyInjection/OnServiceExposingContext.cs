using Enter.ENB.Statics;

namespace Enter.ENB.DependencyInjection;

public class OnServiceExposingContext : IOnServiceExposingContext
{
    public Type ImplementationType { get; }

    public List<Type> ExposedTypes { get; }

    public OnServiceExposingContext(Type implementationType, List<Type> exposedTypes)
    {
        ImplementationType = EntCheck.NotNull(implementationType, nameof(implementationType));
        ExposedTypes = EntCheck.NotNull(exposedTypes, nameof(exposedTypes));
    }
}