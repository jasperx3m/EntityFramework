using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickReach.ECommerce.Infra.Data.Migrations
{
    public partial class Addedsomechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductManufacturer_Manufacturer_ManufacturerID",
                table: "ProductManufacturer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manufacturer",
                table: "Manufacturer");

            migrationBuilder.RenameTable(
                name: "Manufacturer",
                newName: "Manufacturers");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "CartItem",
                newName: "ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manufacturers",
                table: "Manufacturers",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductManufacturer_Manufacturers_ManufacturerID",
                table: "ProductManufacturer",
                column: "ManufacturerID",
                principalTable: "Manufacturers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductManufacturer_Manufacturers_ManufacturerID",
                table: "ProductManufacturer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manufacturers",
                table: "Manufacturers");

            migrationBuilder.RenameTable(
                name: "Manufacturers",
                newName: "Manufacturer");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CartItem",
                newName: "productId");

            migrationBuilder.AlterColumn<string>(
                name: "productId",
                table: "CartItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manufacturer",
                table: "Manufacturer",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductManufacturer_Manufacturer_ManufacturerID",
                table: "ProductManufacturer",
                column: "ManufacturerID",
                principalTable: "Manufacturer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
