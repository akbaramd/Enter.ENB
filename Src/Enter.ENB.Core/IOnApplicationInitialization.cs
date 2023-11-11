using System.Threading.Tasks;
using Enter.ENB.Modularity;

namespace Volo.Abp;

public interface IOnApplicationInitialization
{
    Task OnApplicationInitializationAsync(ApplicationInitializationContext context);

    void OnApplicationInitialization( ApplicationInitializationContext context);
}
