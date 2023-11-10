using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Modularity;

public static class Extensions
{
    public static IServiceCollection AddModularity<TBaseModule>(this IServiceCollection services) where TBaseModule : EntModule
{
    Console.WriteLine($"[Enter.ENB] Start Load Modules");
    
    var moduleProvider = new EntModuleProvider();

    LoadModule(typeof(TBaseModule),0);
    
    void LoadModule(Type moduleType, int stage)
    {
        var nStage = stage + 1;

        if (moduleProvider.Exists(moduleType.Name))
            return;

        // Create a new AssemblyLoadContext for each module
        var alc = new AssemblyLoadContext(moduleType.Name, isCollectible: true);
        
        try
        {
            var moduleAssembly = alc.LoadFromAssemblyPath(moduleType.Assembly.Location);
            
            var module = Activator.CreateInstance(moduleAssembly.GetType(moduleType.FullName)) as EntModule;

            module?.PreConfigureServices();
            module?.ConfigureServices(services);
            module?.PostConfigureServices();
            
            // module.ConfigureApp(app);
            if (module == null) return;
            
            moduleProvider.Register(module);

            var space = "";
            for (int i = 1; i < nStage; i++)
            {
                space += "  ";
            }

            Console.WriteLine($"[Enter.ENB] {space}- {moduleType.Name}");
            
            // Check if module has DependsOnModulesAttribute
            var dependsOnModulesAttribute = moduleType.GetCustomAttribute<DependsOnModulesAttribute>();
            if (dependsOnModulesAttribute != null)
            {
                foreach (var dependencyType in dependsOnModulesAttribute.Modules)
                {
                    // Load dependent modules in the same context
                    LoadModule(dependencyType, nStage);
                }
            }

        }
        finally
        {
            // Unload the AssemblyLoadContext to release resources
            alc.Unload();
        }
    }

    return services;
}

}