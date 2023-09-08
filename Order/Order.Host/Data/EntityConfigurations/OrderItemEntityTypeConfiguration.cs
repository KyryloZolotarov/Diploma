using Order.Hosts.Data.Entities;

namespace Order.Hosts.Data.EntityConfigurations
{
    public class OrderItemEntityTypeConfiguration
        : IEntityTypeConfiguration<OrderItemEntity>
    {
        public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
        {
            builder.ToTable("Item");

            builder.Property(ci => ci.Id)
                .IsRequired(true);

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.Price)
                .IsRequired(true);

            builder.Property(ci => ci.CatalogSubTypeId)
                .IsRequired(true);

            builder.Property(ci => ci.CatalogModelId)
                .IsRequired(true);

            builder.Property(ci => ci.Count)
                .IsRequired(true);

            builder.HasOne(ci => ci.Order)
                .WithMany()
                .HasForeignKey(ci => ci.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
