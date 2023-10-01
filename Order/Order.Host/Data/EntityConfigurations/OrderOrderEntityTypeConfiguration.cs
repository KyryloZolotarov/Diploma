using Order.Hosts.Data.Entities;

namespace Order.Hosts.Data.EntityConfigurations;

public class OrderOrderEntityTypeConfiguration
    : IEntityTypeConfiguration<OrderOrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderOrderEntity> builder)
    {
        builder.ToTable("Order");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .UseHiLo("order_order_hilo")
            .IsRequired();

        builder.Property(ci => ci.DateTime)
            .IsRequired();

        builder.Property(cb => cb.UserId)
            .IsRequired();

        builder.HasOne(ci => ci.User)
            .WithMany()
            .HasForeignKey(ci => ci.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}