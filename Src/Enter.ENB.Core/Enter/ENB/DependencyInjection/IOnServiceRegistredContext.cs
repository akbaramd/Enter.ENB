using Enter.ENB.Collections;
using Enter.ENB.DynamicProxy;

namespace Enter.ENB.DependencyInjection;

public interface IOnServiceRegistredContext
{
    ITypeList<IEntInterceptor> Interceptors { get; }

    Type ImplementationType { get; }
}