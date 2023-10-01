using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Host.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "catalog_brand_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "catalog_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "catalog_model_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "catalog_subtype_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "catalog_type_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "CatalogBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Brand = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Model = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CatalogBrandId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogModel_CatalogBrand_CatalogBrandId",
                        column: x => x.CatalogBrandId,
                        principalTable: "CatalogBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogSubType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    SubType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CatalogTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogSubType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogSubType_CatalogType_CatalogTypeId",
                        column: x => x.CatalogTypeId,
                        principalTable: "CatalogType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PictureFileName = table.Column<string>(type: "text", nullable: true),
                    PartNumber = table.Column<string>(type: "text", nullable: true),
                    CatalogSubTypeId = table.Column<int>(type: "integer", nullable: false),
                    CatalogModelId = table.Column<int>(type: "integer", nullable: false),
                    AvailableStock = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalog_CatalogModel_CatalogModelId",
                        column: x => x.CatalogModelId,
                        principalTable: "CatalogModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Catalog_CatalogSubType_CatalogSubTypeId",
                        column: x => x.CatalogSubTypeId,
                        principalTable: "CatalogSubType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CatalogModelId",
                table: "Catalog",
                column: "CatalogModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CatalogSubTypeId",
                table: "Catalog",
                column: "CatalogSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogModel_CatalogBrandId",
                table: "CatalogModel",
                column: "CatalogBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogSubType_CatalogTypeId",
                table: "CatalogSubType",
                column: "CatalogTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "CatalogModel");

            migrationBuilder.DropTable(
                name: "CatalogSubType");

            migrationBuilder.DropTable(
                name: "CatalogBrand");

            migrationBuilder.DropTable(
                name: "CatalogType");

            migrationBuilder.DropSequence(
                name: "catalog_brand_hilo");

            migrationBuilder.DropSequence(
                name: "catalog_hilo");

            migrationBuilder.DropSequence(
                name: "catalog_model_hilo");

            migrationBuilder.DropSequence(
                name: "catalog_subtype_hilo");

            migrationBuilder.DropSequence(
                name: "catalog_type_hilo");
        }
    }
}
