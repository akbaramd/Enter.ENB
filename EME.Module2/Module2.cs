using Enter.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EME.Module2;

[DependsOnModules(
    typeof(Module1.Module1)
)]
public class Module2 : IEntModule
{
    public void Configure(IServiceCollection services)
    {
        throw new NotImplementedException();
    }

    public void ConfigureApp(IApplicationBuilder services)
    {
        throw new NotImplementedException();
    }
}