using Enter.ENB.Core.Exceptions;
using Enter.ENB.Extensions;
using Enter.ENB.Statics;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Modularity;

public class ModuleLoader : IModuleLoader
{
    public IEntModuleDescriptor[] LoadModules(
        IServiceCollection services,
        Type startupModuleType)
    {
        EntCheck.NotNull(services, nameof(services));
        EntCheck.NotNull(startupModuleType, nameof(startupModuleType));

        var modules = GetDescriptors(services, startupModuleType);

        modules = SortByDependency(modules, startupModuleType);

        return modules.ToArray();
    }

    private List<IEntModuleDescriptor> GetDescriptors(
        IServiceCollection services,
        Type startupModuleType)
    {
        var modules = new List<EntModuleDescriptor>();

        FillModules(modules, services, startupModuleType);
        SetDependencies(modules);

        return modules.Cast<IEntModuleDescriptor>().ToList();
    }

    protected virtual void FillModules(
        List<EntModuleDescriptor> modules,
        IServiceCollection services,
        Type startupModuleType)
    {

        //All modules starting from the startup module
        foreach (var moduleType in AbpModuleHelper.FindAllModuleTypes(startupModuleType))
        {
            modules.Add(CreateModuleDescriptor(services, moduleType));
        }

        // //Plugin modules
        // foreach (var moduleType in plugInSources.GetAllModules())
        // {
        //     if (modules.Any(m => m.Type == moduleType))
        //     {
        //         continue;
        //     }
        //
        //     modules.Add(CreateModuleDescriptor(services, moduleType, isLoadedAsPlugIn: true));
        // }
    }

    protected virtual void SetDependencies(List<EntModuleDescriptor> modules)
    {
        foreach (var module in modules)
        {
            SetDependencies(modules, module);
        }
    }

    protected virtual List<IEntModuleDescriptor> SortByDependency(List<IEntModuleDescriptor> modules, Type startupModuleType)
    {
        var sortedModules = modules.SortByDependencies(m => m.Dependencies);
        sortedModules.MoveItem(m => m.Type == startupModuleType, modules.Count - 1);
        return sortedModules;
    }

    protected virtual EntModuleDescriptor CreateModuleDescriptor(IServiceCollection services, Type moduleType, bool isLoadedAsPlugIn = false)
    {
        return new EntModuleDescriptor(moduleType, CreateAndRegisterModule(services, moduleType), isLoadedAsPlugIn);
    }

    protected virtual IEntModule CreateAndRegisterModule(IServiceCollection services, Type moduleType)
    {
        var module = (IEntModule)Activator.CreateInstance(moduleType)!;
        services.AddSingleton(moduleType, module);
        return module;
    }

    protected virtual void SetDependencies(List<EntModuleDescriptor> modules, EntModuleDescriptor module)
    {
        foreach (var dependedModuleType in AbpModuleHelper.FindDependedModuleTypes(module.Type))
        {
            var dependedModule = modules.FirstOrDefault(m => m.Type == dependedModuleType);
            if (dependedModule == null)
            {
                throw new EntException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + module.Type.AssemblyQualifiedName);
            }

            module.AddDependency(dependedModule);
        }
    }
}
