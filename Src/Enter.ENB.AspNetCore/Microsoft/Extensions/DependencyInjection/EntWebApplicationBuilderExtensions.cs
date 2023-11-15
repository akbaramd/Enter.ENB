using System;
using System.Threading.Tasks;
using Enter.ENB;
using Enter.ENB.Modularity;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection;

public static class EntWebApplicationBuilderExtensions
{
    public static Task<IEntApplicationServiceProvider> AddApplicationAsync<TStartupModule>(
        this WebApplicationBuilder builder)
        where TStartupModule : IEntModule
    {
        return  builder.Services.AddApplicationAsync<TStartupModule>();
    }

    public static Task<IEntApplicationServiceProvider> AddApplicationAsync(
        this WebApplicationBuilder builder,
        Type startupModuleType)
    {
        return  builder.Services.AddApplicationAsync(startupModuleType);
    }
}