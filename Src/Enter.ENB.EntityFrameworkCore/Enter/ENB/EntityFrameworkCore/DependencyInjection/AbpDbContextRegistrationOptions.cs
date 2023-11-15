using Enter.ENB.DependencyInjection;
using Enter.ENB.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.EntityFrameworkCore.DependencyInjection;

public class AbpDbContextRegistrationOptions : AbpCommonDbContextRegistrationOptions, IAbpDbContextRegistrationOptionsBuilder
{
    public Dictionary<Type, object> AbpEntityOptions { get; }

    public AbpDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
        : base(originalDbContextType, services)
    {
        AbpEntityOptions = new Dictionary<Type, object>();
    }

    public void Entity<TEntity>(Action<AbpEntityOptions<TEntity>> optionsAction) where TEntity : IEntEntity
    {
        Services.Configure<AbpEntityOptions>(options =>
        {
            options.Entity(optionsAction);
        });
    }

    
}