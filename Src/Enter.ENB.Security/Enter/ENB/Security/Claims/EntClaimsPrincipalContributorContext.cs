using System.Security.Claims;

namespace Enter.ENB.Security.Claims;

public class EntClaimsPrincipalContributorContext
{
    public ClaimsPrincipal ClaimsPrincipal { get; }

    public IServiceProvider ServiceProvider { get; }

    public EntClaimsPrincipalContributorContext(
         ClaimsPrincipal claimsIdentity,
         IServiceProvider serviceProvider)
    {
        ClaimsPrincipal = claimsIdentity;
        ServiceProvider = serviceProvider;
    }
}