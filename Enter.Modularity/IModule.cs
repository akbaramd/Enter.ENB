using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.Modularity;

public interface IEntModule
{
    public void Configure(IServiceCollection services);
    public void ConfigureApp(IApplicationBuilder services);
}