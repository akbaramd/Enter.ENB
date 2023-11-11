using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Enter.ENB.Modularity;

namespace Volo.Abp;

public interface IOnApplicationShutdown
{
    Task OnApplicationShutdownAsync(ApplicationShutdownContext context);

    void OnApplicationShutdown( ApplicationShutdownContext context);
}
