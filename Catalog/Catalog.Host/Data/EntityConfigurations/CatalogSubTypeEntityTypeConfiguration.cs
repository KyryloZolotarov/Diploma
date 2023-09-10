using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations;

public class CatalogSubTypeEntityTypeConfiguration
    : IEntityTypeConfiguration<CatalogSubType>
{
    public void Configure(EntityTypeBuilder<CatalogSubType> builder)
    {
        builder.ToTable("CatalogSubType");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .UseHiLo("catalog_subtype_hilo")
            .IsRequired();

        builder.Property(cb => cb.SubType)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(ci => ci.CatalogType)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}