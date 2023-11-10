using System.Runtime.CompilerServices;

namespace Enter.ENB.Domain.Entities;

[Serializable]
public abstract class EntEntity : IEntEntity
{
    protected EntEntity()
    {
        EntityHelper.TrySetTenantId(this);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[ENTITY: {GetType().Name}] Keys = {string.Join(',',GetKeys())}";
    }

    public abstract object?[] GetKeys();

    public bool EntityEquals(IEntEntity other)
    {
        return EntityHelper.EntityEquals(this, other);
    }
}

/// <inheritdoc cref="IEntity{TKey}" />
[Serializable]
public abstract class EntEntity<TKey> : EntEntity, IEntEntity<TKey>
{
    /// <inheritdoc/>
    public virtual TKey Id { get; protected set; } = default!;

    protected EntEntity()
    {

    }

    protected EntEntity(TKey id)
    {
        Id = id;
    }

    public override object?[] GetKeys()
    {
        return new object?[] { Id };
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[ENTITY: {GetType().Name}] Id = {Id}";
    }
}