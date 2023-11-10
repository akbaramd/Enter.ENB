namespace Enter.ENB.Uow;

public interface IHasConcurrencyStamp
{
    string ConcurrencyStamp { get; set; }
}