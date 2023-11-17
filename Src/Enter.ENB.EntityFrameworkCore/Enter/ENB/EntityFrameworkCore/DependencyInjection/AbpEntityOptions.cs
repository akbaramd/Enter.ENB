using Enter.ENB.Domain.Entities;
using Enter.ENB.Extensions;
using Enter.ENB.Statics;

namespace Enter.ENB.EntityFrameworkCore.DependencyInjection;

public class EntEntityOptions<TEntity>
    where TEntity : IEntEntity
{
    public static EntEntityOptions<TEntity> Empty { get; } = new EntEntityOptions<TEntity>();

    public Func<IQueryable<TEntity>, IQueryable<TEntity>>? DefaultWithDetailsFunc { get; set; }
}

public class EntEntityOptions
{
    private readonly IDictionary<Type, object> _options;

    public EntEntityOptions()
    {
        _options = new Dictionary<Type, object>();
    }

    public EntEntityOptions<TEntity>? GetOrNull<TEntity>()
        where TEntity : IEntEntity
    {
        return _options.GetOrDefault(typeof(TEntity)) as EntEntityOptions<TEntity>;
    }

    public void Entity<TEntity>(Action<EntEntityOptions<TEntity>> optionsAction)
        where TEntity : IEntEntity
    {
        EntCheck.NotNull(optionsAction, nameof(optionsAction));

        optionsAction(
            (_options.GetOrAdd(
                typeof(TEntity),
                () => new EntEntityOptions<TEntity>()
            ) as EntEntityOptions<TEntity>)!
        );
    }
}
