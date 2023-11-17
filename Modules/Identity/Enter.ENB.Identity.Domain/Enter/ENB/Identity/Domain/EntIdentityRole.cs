using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Enter.ENB.Domain.Auditing;
using Enter.ENB.Domain.Entities;
using Enter.ENB.MultiTenancy;
using Enter.ENB.Statics;
using Enter.ENB.Identity;

namespace Enter.ENB.Identity.Domain;

public  class EntIdentityRole : AggregateRoot<Guid>,IMultiTenant
{
    private EntIdentityRole(){}
    
    public EntIdentityRole(Guid id,string name,string title)
    {

        Id = id;
        SetName(name);
        SetTitle(title);
        Users = new List<EntIdentityUser>();
    }
    
    public EntIdentityRole(Guid id,string name)
    {
        Id = id;
        SetName(name);
        SetTitle(name);
        Users = new List<EntIdentityUser>();
    }
    public string Name { get; private set; }
    public string Title { get; private  set; }
    
    public virtual ICollection<EntIdentityRoleClaim> Claims { get; protected set; }
    public virtual ICollection<EntIdentityUser> Users { get; private set; }
    
    public virtual void AddClaim(Claim claim)
    {
        EntCheck.NotNull(claim, nameof(claim));

        Claims.Add(new EntIdentityRoleClaim(Guid.NewGuid(), Id, claim, TenantId));
    }

    public virtual void AddClaims( IEnumerable<Claim> claims)
    {
        EntCheck.NotNull(claims, nameof(claims));

        foreach (var claim in claims)
        {
            AddClaim(claim);
        }
    }

    public virtual EntIdentityRoleClaim FindClaim([NotNull] Claim claim)
    {
        EntCheck.NotNull(claim, nameof(claim));

        return Claims.FirstOrDefault(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
    }

    public virtual void RemoveClaim([NotNull] Claim claim)
    {
        EntCheck.NotNull(claim, nameof(claim));

        Claims.RemoveAll(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
    }

    public virtual void ChangeName(string name)
    {
        EntCheck.NotNullOrWhiteSpace(name, nameof(name));

        var oldName = Name;
        Name = name;
    }
    
    public void SetName(string name)
    {
        EntCheck.NotNullOrWhiteSpace(name, nameof(name));
        Name = name.Trim();
    }
    
    public void SetTitle(string title)
    {
        EntCheck.NotNullOrWhiteSpace(title, nameof(title));
        Title = title.Trim();
    }

    public Guid? TenantId { get; }
}