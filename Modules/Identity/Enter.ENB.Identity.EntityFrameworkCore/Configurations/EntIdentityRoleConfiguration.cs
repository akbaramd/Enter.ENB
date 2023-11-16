using Enter.ENB.Data;
using Enter.ENB.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Enter.ENB.Identity.EntityFrameworkCore.Configurations;

public class EntIdentityRoleConfiguration : IEntityTypeConfiguration<EntIdentityRole>
{
    public void Configure(EntityTypeBuilder<EntIdentityRole> builder )
    {
        builder.ToTable("EntRoles");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Title).IsUnique();
        builder.HasIndex(x => x.Name).IsUnique();
        
        builder
            .Property(b => b.EntExtraProperties)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<EntExtraPropertyDictionary>(v) ?? new EntExtraPropertyDictionary());
    }
}