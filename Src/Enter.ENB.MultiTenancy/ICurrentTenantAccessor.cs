namespace Enter.ENB.MultiTenancy;

public interface ICurrentTenantAccessor
{
    BasicTenantInfo? Current { get; set; }
}
