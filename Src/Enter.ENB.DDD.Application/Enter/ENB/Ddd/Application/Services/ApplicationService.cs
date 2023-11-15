using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Enter.ENB.Application.Services;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Linq;
using Enter.ENB.ObjectMapping;

namespace Volo.Abp.Application.Services;

public abstract class ApplicationService :
    IApplicationService,
    ITransientDependency
{
    protected ApplicationService(IEntLazyServiceProvider lazyServiceProvider)
    {
        LazyServiceProvider = lazyServiceProvider;
    }

    public IEntLazyServiceProvider LazyServiceProvider { get; set; } 

    protected IAsyncQueryableExecuter AsyncExecuter => LazyServiceProvider.LazyGetRequiredService<IAsyncQueryableExecuter>();

    protected Type? ObjectMapperContext { get; set; }
    protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetService<IObjectMapper>(provider =>
        ObjectMapperContext == null
            ? provider.GetRequiredService<IObjectMapper>()
            : (IObjectMapper)provider.GetRequiredService(typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext)));

    protected ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();

    protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName!) ?? NullLogger.Instance);

    
}
