using Enter.ENB.Domain.Entities;
using Enter.ENB.Extensions;
using Enter.ENB.Statics;

namespace Enter.ENB.EntityFrameworkCore.DependencyInjection;

public class AbpEntityOptions<TEntity>
    where TEntity : IEntEntity
{
    public static AbpEntityOptions<TEntity> Empty { get; } = new AbpEntityOptions<TEntity>();

    public Func<IQueryable<TEntity>, IQueryable<TEntity>>? DefaultWithDetailsFunc { get; set; }
}

public class AbpEntityOptions
{
    private readonly IDictionary<Type, object> _options;

    public AbpEntityOptions()
    {
        _options = new Dictionary<Type, object>();
    }

    public AbpEntityOptions<TEntity>? GetOrNull<TEntity>()
        where TEntity : IEntEntity
    {
        return _options.GetOrDefault(typeof(TEntity)) as AbpEntityOptions<TEntity>;
    }

    public void Entity<TEntity>(Action<AbpEntityOptions<TEntity>> optionsAction)
        where TEntity : IEntEntity
    {
        EntCheck.NotNull(optionsAction, nameof(optionsAction));

        optionsAction(
            (_options.GetOrAdd(
                typeof(TEntity),
                () => new AbpEntityOptions<TEntity>()
            ) as AbpEntityOptions<TEntity>)!
        );
    }
}
