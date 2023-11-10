namespace Enter.ENB.Auditing;

public interface IModificationAuditedObject : IHasModificationTime
{
    /// <summary>Last modifier user for this entity.</summary>
    Guid? LastModifierId { get; }
}