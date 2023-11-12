using System.Diagnostics.CodeAnalysis;
using Enter.ENB.Statics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB;

public class AbpApplicationCreationOptions
{
    public IServiceCollection Services { get; }

    // public PlugInSourceList PlugInSources { get; }

    /// <summary>
    /// The options in this property only take effect when IConfiguration not registered.
    /// </summary>
    public AbpConfigurationBuilderOptions Configuration { get; }

    public bool SkipConfigureServices { get; set; }

    public string? ApplicationName { get; set; }

    public string? Environment { get; set; }

    public AbpApplicationCreationOptions([NotNull] IServiceCollection services)
    {
        Services = EntCheck.NotNull(services, nameof(services));
        // PlugInSources = new PlugInSourceList();
        Configuration = new AbpConfigurationBuilderOptions();
    }
}