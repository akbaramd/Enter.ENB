using Enter.ENB.AutoMapper;
using Enter.ENB.Ddd.Application;
using Enter.ENB.Domain;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Identity.Application;

[DependsOnModules(
    typeof(EntAutoMapperModule),
    typeof(EntDddDomainModule),
    typeof(EntDddApplicationContractsModule),
    typeof(EntDddApplicationModule)
    )]
public class EntIdentityApplicationContractsModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<EntIdentityApplicationContractsModule>();

        Configure<EntAutoMapperOptions>(options =>
        {
            options.AddProfile<EntIdentityMapperProfile>(validate: true);
        });
    }
}