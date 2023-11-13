namespace Enter.ENB.DynamicProxy;

public interface IEntInterceptor
{
    Task InterceptAsync(IEntMethodInvocation invocation);
}