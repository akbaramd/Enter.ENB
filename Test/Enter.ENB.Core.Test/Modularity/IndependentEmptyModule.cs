using Enter;
using Enter.ENB.Modularity;

namespace Modularity;

public class IndependentEmptyModule : EntTestModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SkipAutoServiceRegistration = true;
        base.PreConfigureServices(context);
    }

  
}
