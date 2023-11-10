namespace Enter.ENB.Auditing;
public interface IMayHaveCreator
{
    /// <summary>Id of the creator.</summary>
    Guid? CreatorId { get; }
}