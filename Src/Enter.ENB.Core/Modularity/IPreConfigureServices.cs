using System.Threading.Tasks;
using Enter.ENB.Core.Modularity;

namespace Volo.Abp.Modularity;

public interface IPreConfigureServices
{
    Task PreConfigureServicesAsync(ServiceConfigurationContext context);

    void PreConfigureServices(ServiceConfigurationContext context);
}
