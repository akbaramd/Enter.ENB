using System.Threading.Tasks;
using Enter.ENB.Core.Modularity;

namespace Volo.Abp.Modularity;

public interface IPostConfigureServices
{
    Task PostConfigureServicesAsync(ServiceConfigurationContext context);

    void PostConfigureServices(ServiceConfigurationContext context);
}
