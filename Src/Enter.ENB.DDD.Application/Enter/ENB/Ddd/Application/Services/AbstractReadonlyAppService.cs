using Microsoft.Extensions.Logging;

namespace Enter.ENB.Ddd.Application.Services;

public abstract class AbstractReadonlyAppService : ApplicationService 
{
    protected AbstractReadonlyAppService(ILogger<ApplicationService> logger) : base(logger)
    {
    }
}