using Enter.ENB.DependencyInjection;
using Enter.ENB.ObjectMapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Enter.ENB.Ddd.Application.Services;

public abstract class ApplicationService :
    IApplicationService,
    ITransientDependency
{
    private IEntLazyServiceProvider _lazyServiceProvider; 
    public ILogger<ApplicationService> Logger { get; set; }

    public ApplicationService(IEntLazyServiceProvider lazyServiceProvider)
    {
        _lazyServiceProvider = lazyServiceProvider;
    }
    protected Type? ObjectMapperContext { get; set; }
    protected IObjectMapper ObjectMapper => _lazyServiceProvider.LazyGetService<IObjectMapper>(provider =>
        ObjectMapperContext == null
            ? provider.GetRequiredService<IObjectMapper>()
            : (IObjectMapper)provider.GetRequiredService(typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext)));
    
    
}