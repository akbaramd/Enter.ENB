using Enter.ENB.Identity.Domain;
using Enter.ENB.Identity.EntityFrameworkCore.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Identity.EntityFrameworkCore;

public static class EfCoreModelBuilderExtensions
{
    public static void ConfigureEntIdentityUser(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new EntIdentityUserConfiguration());
    }
    
    public static void ConfigureEntIdentityRole(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new EntIdentityRoleConfiguration());
    }
    
    public static void ConfigureEntIdentityClaim(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new EntIdentityClaimsConfiguration());
    }
}