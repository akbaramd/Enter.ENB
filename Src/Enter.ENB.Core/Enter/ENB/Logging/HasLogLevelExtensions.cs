using Enter.ENB.Statics;
using Microsoft.Extensions.Logging;

namespace Enter.ENB.Logging;

public static class HasLogLevelExtensions
{
    public static TException WithLogLevel<TException>(this TException exception, LogLevel logLevel)
        where TException : IHasLogLevel
    {
        EntCheck.NotNull(exception, nameof(exception));

        exception.LogLevel = logLevel;

        return exception;
    }
}
