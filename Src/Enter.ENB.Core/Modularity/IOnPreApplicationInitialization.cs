using System.Threading.Tasks;
using Enter.ENB.Modularity;

namespace Volo.Abp.Modularity;

public interface IOnPreApplicationInitialization
{
    Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context);

    void OnPreApplicationInitialization(ApplicationInitializationContext context);
}
