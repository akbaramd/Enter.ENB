namespace Enter.ENB.ExceptionHandling;

public interface IExceptionSubscriber
{
    Task HandleAsync( ExceptionNotificationContext context);
}
