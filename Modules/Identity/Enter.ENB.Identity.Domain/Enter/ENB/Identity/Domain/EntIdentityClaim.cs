using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Enter.ENB.Domain.Entities;
using Enter.ENB.MultiTenancy;
using Enter.ENB.Statics;

namespace Enter.ENB.Identity;

public abstract class EntIdentityClaim : EntEntity<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// Gets or sets the claim type for this claim.
    /// </summary>
    public virtual string ClaimType { get; protected set; }

    /// <summary>
    /// Gets or sets the claim value for this claim.
    /// </summary>
    public virtual string ClaimValue { get; protected set; }

    protected EntIdentityClaim()
    {

    }

    protected internal EntIdentityClaim(Guid id, Claim claim, Guid? tenantId)
        : this(id, claim.Type, claim.Value, tenantId)
    {

    }

    protected internal EntIdentityClaim(Guid id, string claimType, string claimValue, Guid? tenantId)
    {
        EntCheck.NotNull(claimType, nameof(claimType));

        Id = id;
        ClaimType = claimType;
        ClaimValue = claimValue;
        TenantId = tenantId;
    }

    public virtual Claim ToClaim()
    {
        return new Claim(ClaimType, ClaimValue);
    }
    
    public virtual void SetClaim(Claim claim)
    {
        EntCheck.NotNull(claim, nameof(claim));

        ClaimType = claim.Type;
        ClaimValue = claim.Value;
    }
    
}
