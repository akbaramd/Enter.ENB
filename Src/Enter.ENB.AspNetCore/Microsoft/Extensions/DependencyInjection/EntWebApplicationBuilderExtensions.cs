using System;
using System.Threading.Tasks;
using Enter.ENB;
using Enter.ENB.Modularity;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection;

public static class EntWebApplicationBuilderExtensions
{
    public static Task<IEntApplicationWithExternalServiceProvider> AddApplicationAsync<TStartupModule>(
        this WebApplicationBuilder builder,
        Action<EntApplicationCreationOptions>? action = null)
        where TStartupModule : IEntModule
    {
        return  builder.Services.AddApplicationAsync<TStartupModule>(action);
    }

    public static Task<IEntApplicationWithExternalServiceProvider> AddApplicationAsync(
        this WebApplicationBuilder builder,
        Type startupModuleType,
        Action<EntApplicationCreationOptions>? action = null)
    {
        return  builder.Services.AddApplicationAsync(startupModuleType,action);
    }
}