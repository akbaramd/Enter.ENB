using Enter.ENB.Data;
using Enter.ENB.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Enter.ENB.Identity.EntityFrameworkCore.Configurations;

public class EntIdentityUserConfiguration : IEntityTypeConfiguration<EntIdentityUser>
{
    public void Configure(EntityTypeBuilder<EntIdentityUser> builder )
    {
        builder.ToTable("EntUsers");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.UserName).IsUnique();
        builder.HasIndex(x => x.NormalizedUserName).IsUnique();
        
        builder.HasIndex(x => x.PhoneNumber).IsUnique(false);
        builder.HasIndex(x => x.NormalizedPhoneNumber).IsUnique(false);

        builder.Property(x => x.PhoneNumber).IsRequired(false);
        builder.Property(x => x.NormalizedPhoneNumber).IsRequired(false);
        
        builder
            .Property(b => b.EntExtraProperties)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<EntExtraPropertyDictionary>(v) ?? new EntExtraPropertyDictionary());
    }
}