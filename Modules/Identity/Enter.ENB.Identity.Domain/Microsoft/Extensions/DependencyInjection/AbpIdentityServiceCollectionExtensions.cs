using Enter.ENB.Identity;
using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.Domain.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class EntIdentityServiceCollectionExtensions
{
    public static IdentityBuilder AddEntIdentity(this IServiceCollection services)
    {
        return services.AddEntIdentity(setupAction: null);
    }

    public static IdentityBuilder AddEntIdentity(this IServiceCollection services, Action<IdentityOptions> setupAction)
    {
        //EntRoleManager
        services.TryAddScoped<IdentityRoleManager>();
        services.TryAddScoped(typeof(RoleManager<EntIdentityRole>), provider => provider.GetService(typeof(IdentityRoleManager)));

        //EntUserManager
        services.TryAddScoped<IdentityUserManager>();
        services.TryAddScoped(typeof(UserManager<EntIdentityUser>), provider => provider.GetService(typeof(IdentityUserManager)));

        return services
            .AddIdentityCore<EntIdentityUser>(setupAction)
            .AddRoles<EntIdentityRole>();
        // .AddClaimsPrincipalFactory<EntUserClaimsPrincipalFactory>();
    }
}