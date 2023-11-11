namespace Enter.ENB.Core;

public interface IApplicationInfoAccessor
{
    string? ApplicationName { get; }
    string ApplicationInstance { get; }
}