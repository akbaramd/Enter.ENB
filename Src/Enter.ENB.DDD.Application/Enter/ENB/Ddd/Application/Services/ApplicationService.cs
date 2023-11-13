using Enter.ENB.DependencyInjection;
using Enter.ENB.Exceptions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Enter.ENB.Ddd.Application.Services;

public abstract class ApplicationService :
    IApplicationService,
    ITransientDependency
{
    public ILogger<ApplicationService> Logger { get; set; }

    protected ApplicationService(ILogger<ApplicationService> logger)
    {
        Logger = logger;
    }
}