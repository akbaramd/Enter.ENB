namespace Enter.ENB.Modularity;

public class DependsOnModulesAttribute : Attribute , IDependedTypesProvider
{
    public Type[] DependedTypes { get; }

    public DependsOnModulesAttribute(params Type[]? dependedTypes)
    {
        DependedTypes = dependedTypes ?? Type.EmptyTypes;
    }

    public virtual Type[] GetDependedTypes()
    {
        return DependedTypes;
    }
}