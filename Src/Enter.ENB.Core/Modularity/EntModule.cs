using System.Reflection;
using Enter.ENB.Core.Modularity;

namespace Enter.ENB.Modularity;

public abstract class EntModule : IEntModule
{
    internal static void CheckAbpModuleType(Type moduleType)
    {
        if (!IsAbpModule(moduleType))
        {
            throw new ArgumentException("Given type is not an ABP module: " + moduleType.AssemblyQualifiedName);
        }
    }
    
    public static bool IsAbpModule(Type type)
    {
        var typeInfo = type.GetTypeInfo();

        return
            typeInfo.IsClass &&
            !typeInfo.IsAbstract &&
            !typeInfo.IsGenericType &&
            typeof(IEntModule).GetTypeInfo().IsAssignableFrom(type);
    }

    public ServiceConfigurationContext ServiceConfigurationContext { get; set; }
    public virtual void ConfigureServices(ServiceConfigurationContext context)
    {
        
    }

    public virtual  Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        return Task.CompletedTask;
    }
}