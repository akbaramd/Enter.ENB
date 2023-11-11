namespace Enter.ENB.Data;

public interface IHasConcurrencyStamp
{
    string ConcurrencyStamp { get; set; }
}