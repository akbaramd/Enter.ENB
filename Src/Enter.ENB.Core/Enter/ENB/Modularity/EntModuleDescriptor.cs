using System.Collections.Immutable;
using System.Reflection;
using Enter.ENB.Statics;

namespace Enter.ENB.Modularity;

public class EntModuleDescriptor : IEntModuleDescriptor
{
    private readonly List<IEntModuleDescriptor> _dependencies;

    public EntModuleDescriptor( Type type, IEntModule instance, bool isLoadedAsPlugIn)
    {
        EntCheck.NotNull(type, nameof(type));
        EntCheck.NotNull(instance, nameof(instance));
        EntModule.CheckEntModuleType(type);
        if (!type.GetTypeInfo().IsAssignableFrom(instance.GetType()))
            throw new ArgumentException(
                $"Given module instance ({instance.GetType().AssemblyQualifiedName}) is not an instance of given module type: {type.AssemblyQualifiedName}");
        Type = type;
        Assembly = type.Assembly;
        AllAssemblies = EntModuleHelper.GetAllAssemblies(type);
        Instance = instance;
        IsLoadedAsPlugIn = isLoadedAsPlugIn;
        _dependencies = new List<IEntModuleDescriptor>();
    }

    public Type Type { get; }
    public Assembly Assembly { get; }
    public Assembly[] AllAssemblies { get; }
    public IEntModule Instance { get; }
    public bool IsLoadedAsPlugIn { get; }
    public IReadOnlyList<IEntModuleDescriptor> Dependencies => _dependencies.ToImmutableList();

    public void AddDependency(IEntModuleDescriptor descriptor)
    {
        _dependencies.AddIfNotContains(descriptor);
    }

    public override string ToString()
    {
        return $"[EntModuleDescriptor {Type.FullName}]";
    }
}