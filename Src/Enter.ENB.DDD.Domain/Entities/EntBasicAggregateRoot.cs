using System.Collections.ObjectModel;

namespace Enter.ENB.Domain.Entities;

[Serializable]
public abstract class BasicAggregateRoot : EntEntity,
    IEntAggregateRoot,
    IEntGeneratesDomainEvents
{
    private readonly ICollection<DomainEventRecord> _distributedEvents = new Collection<DomainEventRecord>();
    private readonly ICollection<DomainEventRecord> _localEvents = new Collection<DomainEventRecord>();

    public virtual IEnumerable<DomainEventRecord> GetLocalEvents()
    {
        return _localEvents;
    }

    public virtual IEnumerable<DomainEventRecord> GetDistributedEvents()
    {
        return _distributedEvents;
    }

    public virtual void ClearLocalEvents()
    {
        _localEvents.Clear();
    }

    public virtual void ClearDistributedEvents()
    {
        _distributedEvents.Clear();
    }

    protected virtual void AddLocalEvent(object eventData)
    {
        // _localEvents.Add(new DomainEventRecord(eventData, EventOrderGenerator.GetNext()));
    }

    protected virtual void AddDistributedEvent(object eventData)
    {
        // _distributedEvents.Add(new DomainEventRecord(eventData, EventOrderGenerator.GetNext()));
    }
}

[Serializable]
public abstract class BasicAggregateRoot<TKey> : 
    EntEntity<TKey>,
    IEntAggregateRoot<TKey>,
    IEntEntity<TKey>,
    IEntEntity,
    IEntAggregateRoot,
    IEntGeneratesDomainEvents
{
    private readonly ICollection<DomainEventRecord> _distributedEvents = (ICollection<DomainEventRecord>) new Collection<DomainEventRecord>();
    private readonly ICollection<DomainEventRecord> _localEvents = (ICollection<DomainEventRecord>) new Collection<DomainEventRecord>();

    protected BasicAggregateRoot()
    {
    }

    protected BasicAggregateRoot(TKey id)
        : base(id)
    {
    }

    public virtual IEnumerable<DomainEventRecord> GetLocalEvents() => (IEnumerable<DomainEventRecord>) this._localEvents;

    public virtual IEnumerable<DomainEventRecord> GetDistributedEvents() => (IEnumerable<DomainEventRecord>) this._distributedEvents;

    public virtual void ClearLocalEvents() => this._localEvents.Clear();

    public virtual void ClearDistributedEvents() => this._distributedEvents.Clear();

    // protected virtual void AddLocalEvent(object eventData) => this._localEvents.Add(new DomainEventRecord(eventData, EventOrderGenerator.GetNext()));

    // protected virtual void AddDistributedEvent(object eventData) => this._distributedEvents.Add(new DomainEventRecord(eventData, EventOrderGenerator.GetNext()));
}