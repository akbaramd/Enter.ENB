namespace Enter.ENB.Domain.Entities;

[Serializable]
public abstract class EntEntity : IEntEntity
{
    protected EntEntity()
    {
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[ENTITY: {GetType().Name}] Keys = {string.Join(',',GetKeys())}";
    }

    public abstract object?[] GetKeys();

    public bool EntityEquals(IEntEntity other)
    {
        //Same instances must be considered as equal
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        //Must have a IS-A relation of types or must be same type
        var typeOfEntity1 = this.GetType();
        var typeOfEntity2 = other.GetType();
        if (!typeOfEntity1.IsAssignableFrom(typeOfEntity2) && !typeOfEntity2.IsAssignableFrom(typeOfEntity1))
        {
            return false;
        }

        return true;
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