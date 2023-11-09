using Enter.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EME.Module1;

public class Module1 : IEntModule
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