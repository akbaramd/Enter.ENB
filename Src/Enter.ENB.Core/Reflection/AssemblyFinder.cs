﻿using System.Collections.Immutable;
using System.Reflection;

namespace Enter.ENB.Core.Reflection;

public class AssemblyFinder : IAssemblyFinder
{
    private readonly IModuleContainer _moduleContainer;

    private readonly Lazy<IReadOnlyList<Assembly>> _assemblies;

    public AssemblyFinder(IModuleContainer moduleContainer)
    {
        _moduleContainer = moduleContainer;

        _assemblies = new Lazy<IReadOnlyList<Assembly>>(FindAll, LazyThreadSafetyMode.ExecutionAndPublication);
    }

    public IReadOnlyList<Assembly> Assemblies => _assemblies.Value;

    public IReadOnlyList<Assembly> FindAll()
    {
        var assemblies = new List<Assembly>();

        foreach (var module in _moduleContainer.Modules)
        {
            assemblies.AddRange(module.AllAssemblies);
        }

        return assemblies.Distinct().ToImmutableList();
    }
}
