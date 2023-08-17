using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations
{
    public class CatalogModelEntityTypeConfiguration
    : IEntityTypeConfiguration<CatalogModel>
    {
        public void Configure(EntityTypeBuilder<CatalogModel> builder)
        {
            builder.ToTable("CatalogModel");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .UseHiLo("catalog_model_hilo")
                .IsRequired();

            builder.Property(cb => cb.Model)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(ci => ci.CatalogBrand)
                .WithMany()
                .HasForeignKey(ci => ci.CatalogBrandId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
