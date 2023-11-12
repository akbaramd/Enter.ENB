namespace Enter.ENB.DynamicProxy;

public interface IAbpInterceptor
{
    Task InterceptAsync(IAbpMethodInvocation invocation);
}