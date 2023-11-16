using Enter.ENB.Ddd.Application.Services;
using Enter.ENB.DependencyInjection;

namespace Enter.ENB.Identity.Application;

public abstract class IdentityAppServiceBase : ApplicationService
{
    protected IdentityAppServiceBase(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
    }
}