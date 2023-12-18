using Enter.ENB.DependencyInjection;

namespace Enter.ENB.Guids;


public class SimpleGuidGenerator : IGuidGenerator , ISingletonDependency
{
    public Guid Create()
    {
        return Guid.NewGuid();
    }
}