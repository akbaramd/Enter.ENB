using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.EntityFrameworkCore.DependencyInjection;

public class EntDbContextRegistrationOptions : EntCommonDbContextRegistrationOptions, IEntDbContextRegistrationOptionsBuilder
{
    public Dictionary<Type, object> EntEntityOptions { get; }

    public EntDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
        : base(originalDbContextType, services)
    {
        EntEntityOptions = new Dictionary<Type, object>();
    }

    public void Entity<TEntity>(Action<EntEntityOptions<TEntity>> optionsAction) where TEntity : IEntEntity
    {
        Services.Configure<EntEntityOptions>(options =>
        {
            options.Entity(optionsAction);
        });
    }

    
}