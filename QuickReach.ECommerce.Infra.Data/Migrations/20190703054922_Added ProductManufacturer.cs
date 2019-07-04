using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickReach.ECommerce.Infra.Data.Migrations
{
    public partial class AddedProductManufacturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductManufacturer",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false),
                    ManufacturerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductManufacturer", x => new { x.ManufacturerID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_ProductManufacturer_Manufacturer_ManufacturerID",
                        column: x => x.ManufacturerID,
                        principalTable: "Manufacturer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductManufacturer_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductManufacturer_ProductID",
                table: "ProductManufacturer",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductManufacturer");

            migrationBuilder.DropTable(
                name: "Manufacturer");
        }
    }
}
