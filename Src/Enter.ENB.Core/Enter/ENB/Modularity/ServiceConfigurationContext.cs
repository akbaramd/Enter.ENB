﻿using Enter.ENB.Extensions;
using Enter.ENB.Statics;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Modularity;

public class ServiceConfigurationContext
{
    public  IServiceCollection Services { get; }

    public IDictionary<string, object?> Items { get; }

    /// <summary>
    /// Gets/sets arbitrary named objects those can be stored during
    /// the service registration phase and shared between modules.
    ///
    /// This is a shortcut usage of the <see cref="Items"/> dictionary.
    /// Returns null if given key is not found in the <see cref="Items"/> dictionary.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public object? this[string key] {
        get => Items.GetOrDefault(key);
        set => Items[key] = value;
    }

    public ServiceConfigurationContext(IServiceCollection services)
    {
        Services = EntCheck.NotNull(services, nameof(services));
        Items = new Dictionary<string, object?>();
    }
}