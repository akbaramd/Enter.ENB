namespace Enter.ENB.Auditing;
public interface IHasDeletionTime : ISoftDelete
{
    /// <summary>Deletion time.</summary>
    DateTime? DeletionTime { get; }
}