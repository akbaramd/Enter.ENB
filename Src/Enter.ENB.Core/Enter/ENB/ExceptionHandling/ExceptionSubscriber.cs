using Enter.ENB.DependencyInjection;

namespace Enter.ENB.ExceptionHandling;

[ExposeServices(typeof(IExceptionSubscriber))]
public abstract class ExceptionSubscriber : IExceptionSubscriber, ITransientDependency
{
    public abstract Task HandleAsync(ExceptionNotificationContext context);
}
