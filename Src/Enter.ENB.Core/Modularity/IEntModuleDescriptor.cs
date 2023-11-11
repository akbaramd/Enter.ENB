using System.Reflection;

namespace Enter.ENB.Modularity;

public interface IEntModuleDescriptor
{
    Type Type { get; }
    
    Assembly Assembly { get; }
    
    Assembly[] AllAssemblies { get; }
    
    IEntModule Instance { get; }
    
    bool IsLoadedAsPlugIn { get; }
    
    IReadOnlyList<IEntModuleDescriptor> Dependencies { get; }
}