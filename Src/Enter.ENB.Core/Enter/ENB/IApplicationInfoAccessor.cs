namespace Enter.ENB;

public interface IApplicationInfoAccessor
{
    string? ApplicationName { get; }
    string InstanceId { get; }
}