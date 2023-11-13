using Enter.ENB.Domain;
using Enter.ENB.Modularity;

namespace Enter.ENB.Ddd.Application;

[
    DependsOnModules(
        typeof(EntDddDomainModule),
        typeof(EntDddApplicationContractsModule))]
public class EntDddApplicationModule : EntModule
{
}