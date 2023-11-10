namespace Enter.ENB.Domain.Entities;

public class DomainEventRecord
{
    public object EventData { get; }
    public long EventOrder { get; }

    public DomainEventRecord(object eventData, long eventOrder)
    {
        this.EventData = eventData;
        this.EventOrder = eventOrder;
    }
}