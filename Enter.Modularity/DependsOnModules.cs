namespace Enter.Modularity;

public class DependsOnModulesAttribute : Attribute
{
    public DependsOnModulesAttribute(params Type[] modules)
    {
        Modules = modules;
    }

    public Type[] Modules { get; set; }
}