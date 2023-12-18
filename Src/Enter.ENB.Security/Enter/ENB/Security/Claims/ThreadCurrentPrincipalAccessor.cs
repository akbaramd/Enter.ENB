using System.Security.Claims;
using Enter.ENB.DependencyInjection;

namespace Enter.ENB.Security.Claims;

public class ThreadCurrentPrincipalAccessor : CurrentPrincipalAccessorBase, ISingletonDependency
{
    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        return (Thread.CurrentPrincipal as ClaimsPrincipal)!;
    }
}
