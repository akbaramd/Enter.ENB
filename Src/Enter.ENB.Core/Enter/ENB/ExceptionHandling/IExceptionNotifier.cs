namespace Enter.ENB.ExceptionHandling;

public interface IExceptionNotifier
{
    Task NotifyAsync(ExceptionNotificationContext context);
}
