using Enter.ENB.Data;
using Enter.ENB.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Enter.ENB.Identity.EntityFrameworkCore;

public static class EfCoreModelBuilderExtensions
{
    public static void ConfigureEntUser(this ModelBuilder builder)
    {
        builder.ApplyConfiguration<EntUser>(new EntUserConfiguration());
    }
}

public class EntUserConfiguration : IEntityTypeConfiguration<EntUser>
{
    public void Configure(EntityTypeBuilder<EntUser> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.UserName).IsUnique();
        
        builder
            .Property(b => b.EntExtraProperties)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<EntExtraPropertyDictionary>(v) ?? new EntExtraPropertyDictionary());
    }
}