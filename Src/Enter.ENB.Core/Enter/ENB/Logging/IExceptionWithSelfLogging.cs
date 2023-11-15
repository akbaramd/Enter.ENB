using Microsoft.Extensions.Logging;

namespace Enter.ENB.Logging;

public interface IExceptionWithSelfLogging
{
    void Log(ILogger logger);
}
