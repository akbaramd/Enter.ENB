namespace Enter.ENB.Auditing;

public interface IDeletionAuditedObject : IHasDeletionTime, ISoftDelete
{
    /// <summary>Id of the deleter user.</summary>
    Guid? DeleterId { get; }
}