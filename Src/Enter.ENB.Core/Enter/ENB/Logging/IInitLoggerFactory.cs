namespace Enter.ENB.Logging;

public interface IInitLoggerFactory
{
    IInitLogger<T> Create<T>();
}
