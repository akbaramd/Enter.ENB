using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Enter.ENB.Modularity;

namespace Volo.Abp.Modularity;

public interface IOnPostApplicationInitialization
{
    Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context);

    void OnPostApplicationInitialization(ApplicationInitializationContext context);
}
