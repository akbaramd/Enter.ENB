using Enter.ENB.DependencyInjection;
using Volo.Abp.Application.Services;

namespace Enter.ENB.Identity.Application;

public abstract class IdentityAppServiceBase : ApplicationService
{
    protected IdentityAppServiceBase(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
    }
}