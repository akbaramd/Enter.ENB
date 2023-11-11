using Enter.ENB.Modularity;

namespace Enter.ENB.Core;

public interface IModuleContainer
{
    IReadOnlyCollection<IEntModuleDescriptor> Modules { get; }
}