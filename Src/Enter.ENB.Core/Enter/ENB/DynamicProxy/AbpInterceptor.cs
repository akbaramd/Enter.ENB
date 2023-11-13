namespace Enter.ENB.DynamicProxy;

public abstract class EntInterceptor : IEntInterceptor
{
    public abstract Task InterceptAsync(IEntMethodInvocation invocation);
}