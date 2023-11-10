namespace Enter.ENB.Auditing;

public interface IAuditedObject : 
    ICreationAuditedObject,
    IHasCreationTime,
    IMayHaveCreator,
    IModificationAuditedObject,
    IHasModificationTime
{
}