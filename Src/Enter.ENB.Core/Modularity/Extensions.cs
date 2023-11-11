using System.Reflection;
using System.Runtime.Loader;
using Enter.ENB.Core;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

namespace Enter.ENB.Modularity;

public static class Extensions
{
    public static void AddModularity<TBaseModule>(this IServiceCollection services) where TBaseModule : EntModule
    {
         AbpApplicationFactory.Create<TBaseModule>(services);
    }

}