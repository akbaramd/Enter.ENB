using Enter.ENB.Collections;

namespace Enter.ENB.Modularity;

public class EntModuleLifecycleOptions
{
    public ITypeList<IModuleLifecycleContributor> Contributors { get; }

    public EntModuleLifecycleOptions()
    {
        Contributors = new TypeList<IModuleLifecycleContributor>();
    }
}
