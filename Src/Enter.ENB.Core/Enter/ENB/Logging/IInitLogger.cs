using Microsoft.Extensions.Logging;

namespace Enter.ENB.Logging;

public interface IInitLogger<out T> : ILogger<T>
{
    public List<AbpInitLogEntry> Entries { get; }
}
