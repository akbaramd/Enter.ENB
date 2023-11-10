using Microsoft.Extensions.Logging;

namespace Enter.ENB.Logging;

public interface IHasLogLevel
{
    /// <summary>Log severity.</summary>
    LogLevel LogLevel { get; set; }
}