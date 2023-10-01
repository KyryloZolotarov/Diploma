﻿// <auto-generated />
using Catalog.Host.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Catalog.Host.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("catalog_brand_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("catalog_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("catalog_model_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("catalog_subtype_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("catalog_type_hilo")
                .IncrementsBy(10);

            modelBuilder.Entity("Catalog.Host.Data.Entities.CatalogBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "catalog_brand_hilo");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("CatalogBrand", (string)null);
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.CatalogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "catalog_hilo");

                    b.Property<int>("AvailableStock")
                        .HasColumnType("integer");

                    b.Property<int>("CatalogModelId")
                        .HasColumnType("integer");

                    b.Property<int>("CatalogSubTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PartNumber")
                        .HasColumnType("text");

                    b.Property<string>("PictureFileName")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CatalogModelId");

                    b.HasIndex("CatalogSubTypeId");

                    b.ToTable("Catalog", (string)null);
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.CatalogModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "catalog_model_hilo");

                    b.Property<int>("CatalogBrandId")
                        .HasColumnType("integer");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("CatalogBrandId");

                    b.ToTable("CatalogModel", (string)null);
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.CatalogSubType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "catalog_subtype_hilo");

                    b.Property<int>("CatalogTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("SubType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("CatalogTypeId");

                    b.ToTable("CatalogSubType", (string)null);
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.CatalogType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "catalog_type_hilo");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("CatalogType", (string)null);
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.CatalogItem", b =>
                {
                    b.HasOne("Catalog.Host.Data.Entities.CatalogModel", "CatalogModel")
                        .WithMany()
                        .HasForeignKey("CatalogModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Host.Data.Entities.CatalogSubType", "CatalogSubType")
                        .WithMany()
                        .HasForeignKey("CatalogSubTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatalogModel");

                    b.Navigation("CatalogSubType");
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.CatalogModel", b =>
                {
                    b.HasOne("Catalog.Host.Data.Entities.CatalogBrand", "CatalogBrand")
                        .WithMany()
                        .HasForeignKey("CatalogBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatalogBrand");
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.CatalogSubType", b =>
                {
                    b.HasOne("Catalog.Host.Data.Entities.CatalogType", "CatalogType")
                        .WithMany()
                        .HasForeignKey("CatalogTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatalogType");
                });
#pragma warning restore 612, 618
        }
    }
}
