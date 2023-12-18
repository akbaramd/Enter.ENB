using Enter;
using Enter.ENB.Modularity;

namespace Modularity;

public class IndependentEmptyPlugInModule : EntTestModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SkipAutoServiceRegistration = true;

        base.PreConfigureServices(context);
    }
}
