using Order.Hosts.Data.Entities;

namespace Order.Hosts.Data.EntityConfigurations;

public class OrderItemEntityTypeConfiguration
    : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder.ToTable("Item");

        builder.Property(ci => ci.Id)
            .UseHiLo("order_item_hilo")
            .IsRequired();

        builder.Property(ci => ci.ItemId)
            .IsRequired();

        builder.Property(ci => ci.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ci => ci.Price)
            .IsRequired();

        builder.Property(ci => ci.CatalogSubTypeId)
            .IsRequired();

        builder.Property(ci => ci.CatalogModelId)
            .IsRequired();

        builder.Property(ci => ci.Count)
            .IsRequired();

        builder.HasOne(ci => ci.Order)
            .WithMany()
            .HasForeignKey(ci => ci.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}