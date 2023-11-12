using System.Reflection;

namespace Enter.ENB.Modularity;

internal static class EntModuleHelper
{
    public static List<Type> FindAllModuleTypes(Type startupModuleType)
    {
        var moduleTypes = new List<Type>();
        Console.WriteLine("Loaded Enter.ENB modules:");
        AddModuleAndDependenciesRecursively(moduleTypes, startupModuleType);
        return moduleTypes;
    }

    public static List<Type> FindDependedModuleTypes(Type moduleType)
    {
        EntModule.CheckEntModuleType(moduleType);

        var dependencies = new List<Type>();

        var dependencyDescriptors = moduleType
            .GetCustomAttributes()
            .OfType<DependsOnModulesAttribute>();

        foreach (var descriptor in dependencyDescriptors)
        {
            foreach (var dependedModuleType in descriptor.GetDependedTypes())
            {
                dependencies.AddIfNotContains(dependedModuleType);
            }
        }

        return dependencies;
    }
    
    public static Assembly[] GetAllAssemblies(Type moduleType)
    {
        var assemblies = new List<Assembly>();

        var additionalAssemblyDescriptors = moduleType
            .GetCustomAttributes()
            .OfType<IAdditionalModuleAssemblyProvider>();

        foreach (var descriptor in additionalAssemblyDescriptors)
        {
            foreach (var assembly in descriptor.GetAssemblies())
            {
                assemblies.AddIfNotContains(assembly);
            }
        }
        
        assemblies.Add(moduleType.Assembly);

        return assemblies.ToArray();
    }

    private static void AddModuleAndDependenciesRecursively(
        List<Type> moduleTypes,
        Type moduleType,
        int depth = 0)
    {
        EntModule.CheckEntModuleType(moduleType);

        if (moduleTypes.Contains(moduleType))
        {
            return;
        }

        moduleTypes.Add(moduleType);
        Console.WriteLine( $"{new string(' ', depth * 2)}- {moduleType.FullName}");

        foreach (var dependedModuleType in FindDependedModuleTypes(moduleType))
        {
            AddModuleAndDependenciesRecursively(moduleTypes, dependedModuleType, depth + 1);
        }
    }
}
