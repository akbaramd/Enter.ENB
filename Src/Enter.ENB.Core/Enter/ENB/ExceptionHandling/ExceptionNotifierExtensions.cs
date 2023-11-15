using Enter.ENB.Statics;
using Microsoft.Extensions.Logging;

namespace Enter.ENB.ExceptionHandling;

public static class ExceptionNotifierExtensions
{
    public static Task NotifyAsync(
        this IExceptionNotifier exceptionNotifier,
        Exception exception,
        LogLevel? logLevel = null,
        bool handled = true)
    {
        EntCheck.NotNull(exceptionNotifier, nameof(exceptionNotifier));

        return exceptionNotifier.NotifyAsync(
            new ExceptionNotificationContext(
                exception,
                logLevel,
                handled
            )
        );
    }
}
