using Enter.ENB.Modularity;

namespace Enter.ENB;

public interface IModuleContainer
{
    IReadOnlyCollection<IEntModuleDescriptor> Modules { get; }
}