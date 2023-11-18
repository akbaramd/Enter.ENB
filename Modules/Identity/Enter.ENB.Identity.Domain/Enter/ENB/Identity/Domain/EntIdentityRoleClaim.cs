using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Enter.ENB.Identity.Domain;

/// <summary>
/// Represents a claim that is granted to all users within a role.
/// </summary>
public class EntIdentityRoleClaim : EntIdentityClaim
{
    /// <summary>
    /// Gets or sets the of the primary key of the role associated with this claim.
    /// </summary>
    public virtual Guid RoleId { get; protected set; }

    protected EntIdentityRoleClaim()
    {

    }

    protected internal EntIdentityRoleClaim(
        Guid id,
        Guid roleId,
        [NotNull] Claim claim,
        Guid? tenantId)
        : base(
              id,
              claim,
              tenantId)
    {
        RoleId = roleId;
    }

    public EntIdentityRoleClaim(
        Guid id,
        Guid roleId,
        [NotNull] string claimType,
        string claimValue,
        Guid? tenantId)
        : base(
              id,
              claimType,
              claimValue,
              tenantId)
    {
        RoleId = roleId;
    }
}
