using Enter.ENB.Data;
using Enter.ENB.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Enter.ENB.Identity.EntityFrameworkCore.Configurations;

public class EntIdentityClaimsConfiguration : IEntityTypeConfiguration<EntIdentityClaim>
{
    public void Configure(EntityTypeBuilder<EntIdentityClaim> builder )
    {
        builder.ToTable("EntClaims");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.ClaimType).IsUnique();
        
    }
}