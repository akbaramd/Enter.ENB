using System.Diagnostics.CodeAnalysis;
using Enter.ENB.Extensions;
using Enter.ENB.Statics;

namespace Enter.ENB.EntityFrameworkCore;

public class EntDbContextOptions
{
    
    internal List<Action<EntDbContextConfigurationContext>> DefaultPreConfigureActions { get; }

    internal Action<EntDbContextConfigurationContext>? DefaultConfigureAction { get; set; }

    internal Dictionary<Type, List<object>> PreConfigureActions { get; }

    internal Dictionary<Type, object> ConfigureActions { get; }

    public EntDbContextOptions()
    {
        DefaultPreConfigureActions = new List<Action<EntDbContextConfigurationContext>>();
        PreConfigureActions = new Dictionary<Type, List<object>>();
        ConfigureActions = new Dictionary<Type, object>();
    }
    
     public void PreConfigure(Action<EntDbContextConfigurationContext> action)
    {
        EntCheck.NotNull(action, nameof(action));

        DefaultPreConfigureActions.Add(action);
    }

    public void Configure([NotNull] Action<EntDbContextConfigurationContext> action)
    {
        EntCheck.NotNull(action, nameof(action));

        DefaultConfigureAction = action;
    }

    public bool IsConfiguredDefault()
    {
        return DefaultConfigureAction != null;
    }

    public void PreConfigure<TDbContext>([NotNull] Action<EntDbContextConfigurationContext<TDbContext>> action)
        where TDbContext : EntDbContext<TDbContext>
    {
        EntCheck.NotNull(action, nameof(action));

        var actions = PreConfigureActions.GetOrDefault(typeof(TDbContext));
        if (actions == null)
        {
            PreConfigureActions[typeof(TDbContext)] = actions = new List<object>();
        }

        actions.Add(action);
    }

    public void Configure<TDbContext>([NotNull] Action<EntDbContextConfigurationContext<TDbContext>> action)
        where TDbContext : EntDbContext<TDbContext>
    {
        EntCheck.NotNull(action, nameof(action));

        ConfigureActions[typeof(TDbContext)] = action;
    }

    public bool IsConfigured<TDbContext>()
    {
        return IsConfigured(typeof(TDbContext));
    }

    public bool IsConfigured(Type dbContextType)
    {
        return ConfigureActions.ContainsKey(dbContextType);
    }

 
}