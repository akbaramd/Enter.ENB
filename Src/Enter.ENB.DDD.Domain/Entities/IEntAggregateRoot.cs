namespace Enter.ENB.Domain.Entities;

public interface IEntAggregateRoot : IEntEntity
{
    
}

public interface IEntAggregateRoot<TKey> : IEntEntity<TKey>, IEntAggregateRoot
{
    
}
