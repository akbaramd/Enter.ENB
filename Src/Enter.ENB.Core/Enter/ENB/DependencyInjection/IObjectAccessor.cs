namespace Enter.ENB.DependencyInjection;

public interface IObjectAccessor<out T>
{
    T? Value { get; }
}