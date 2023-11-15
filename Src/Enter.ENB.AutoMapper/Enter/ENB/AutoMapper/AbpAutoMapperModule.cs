using AutoMapper;
using AutoMapper.Internal;
using Enter.ENB.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Enter.ENB.Modularity;
using Enter.ENB.ObjectMapping;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Enter.ENB.AutoMapper;

[DependsOnModules(
    typeof(EntObjectMappingModule),
    typeof(EntAuditingModule)
)]
public class EntAutoMapperModule : EntModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new EntAutoMapperConventionalRegistrar());
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper();

        context.Services.AddSingleton<IConfigurationProvider>(sp =>
        {
            using (var scope = sp.CreateScope())
            {
                var options = scope.ServiceProvider.GetRequiredService<IOptions<EntAutoMapperOptions>>().Value;

                var mapperConfigurationExpression = sp.GetRequiredService<IOptions<MapperConfigurationExpression>>().Value;
                var autoMapperConfigurationContext = new EntAutoMapperConfigurationContext(mapperConfigurationExpression, scope.ServiceProvider);

                foreach (var configurator in options.Configurators)
                {
                    configurator(autoMapperConfigurationContext);
                }
                var mapperConfiguration = new MapperConfiguration(mapperConfigurationExpression);

                foreach (var profileType in options.ValidatingProfiles)
                {
                    mapperConfiguration.Internal().AssertConfigurationIsValid(((Profile)Activator.CreateInstance(profileType)).ProfileName);
                }

                return mapperConfiguration;
            }
        });

        context.Services.AddTransient<IMapper>(sp => sp.GetRequiredService<IConfigurationProvider>().CreateMapper(sp.GetService));

        context.Services.AddTransient<MapperAccessor>(sp => new MapperAccessor()
        {
            Mapper = sp.GetRequiredService<IMapper>()
        });
        context.Services.AddTransient<IMapperAccessor>(provider => provider.GetRequiredService<MapperAccessor>());
    }
}
