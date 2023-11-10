namespace Enter.ENB.Domain.Entities;

public interface IEntEntity
{
    object?[] GetKeys();
}

public interface IEntEntity<TKey> : IEntEntity
{
    TKey Id { get; }
}