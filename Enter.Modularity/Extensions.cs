using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.Modularity;

public static class Extensions
{
    public static IServiceCollection AddModularity(this IServiceCollection services,params Assembly[] assemblies)
    {
        
        var moduleTypes = assemblies.SelectMany(x => x.GetTypes())
            .Where(x => typeof(IEntModule).IsAssignableTo(x));

        foreach (var moduleType in moduleTypes)
        {
            Console.WriteLine("Module : " + moduleType);

            var depends = moduleType.GetCustomAttribute<DependsOnModulesAttribute>();
            if (depends == null) continue;

            foreach (var dependType in depends.Modules) Console.WriteLine("- Depende : " + dependType.Name);
        }


        return services;
    }
}