using Enter.ENB.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Enter.ENB.Ddd.Application.Services;

public abstract class AbstractReadonlyAppService : ApplicationService 
{
    protected AbstractReadonlyAppService(IEntLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
    }
}