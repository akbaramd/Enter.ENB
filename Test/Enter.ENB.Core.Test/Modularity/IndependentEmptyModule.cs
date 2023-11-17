using Enter.ENB.Modularity;
using Enter.ENB.Test;

namespace Enter.ENB.Modularity;

public class IndependentEmptyModule : EntTestModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SkipAutoServiceRegistration = true;
        base.PreConfigureServices(context);
    }

  
}
