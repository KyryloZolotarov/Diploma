using Order.Hosts.Data.Entities;

namespace Order.Hosts.Data.EntityConfigurations;

public class OrderUserEntityTypeConfiguration
    : IEntityTypeConfiguration<OrderUserEntity>
{
    public void Configure(EntityTypeBuilder<OrderUserEntity> builder)
    {
        builder.ToTable("User");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .IsRequired();

        builder.Property(ci => ci.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ci => ci.GivenName)
            .IsRequired(false);

        builder.Property(ci => ci.FamilyName)
            .IsRequired();

        builder.Property(ci => ci.Email)
            .IsRequired();

        builder.Property(ci => ci.Address)
            .IsRequired();
    }
}