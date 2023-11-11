using System.Reflection;

namespace Enter.ENB.Modularity;

public interface IAdditionalModuleAssemblyProvider
{
    Assembly[] GetAssemblies();
}