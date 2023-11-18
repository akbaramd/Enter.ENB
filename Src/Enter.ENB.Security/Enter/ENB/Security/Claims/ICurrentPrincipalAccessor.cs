using System.Security.Claims;

namespace Enter.ENB.Security.Claims;

public interface ICurrentPrincipalAccessor
{
    ClaimsPrincipal Principal { get; }

    IDisposable Change(ClaimsPrincipal principal);
}
