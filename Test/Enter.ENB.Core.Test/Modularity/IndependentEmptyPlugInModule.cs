using Enter.ENB.Test;

namespace Enter.ENB.Modularity;

public class IndependentEmptyPlugInModule : EntTestModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SkipAutoServiceRegistration = true;

        base.PreConfigureServices(context);
    }
}
