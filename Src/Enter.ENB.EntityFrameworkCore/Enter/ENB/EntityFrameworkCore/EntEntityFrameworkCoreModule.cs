using Enter.ENB.Domain;
using Enter.ENB.Domain.Repository;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.EntityFrameworkCore;

public class EntEntityFrameworkCoreModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(typeof(IRepository<,>), typeof(EfCoreRepository<,>));
        context.Services.AddTransient(typeof(IEfCoreRepository<,>), typeof(EfCoreRepository<,>));
    }
}