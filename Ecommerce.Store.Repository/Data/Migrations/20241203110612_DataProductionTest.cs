using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Store.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataProductionTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Product_ProductId",
                table: "OrderItems",
                newName: "Product_Id");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Product_ProductName",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Product_PictureUrl",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Product_Name",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Product_Name",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Product_Id",
                table: "OrderItems",
                newName: "Product_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "Product_ProductName",
                table: "OrderItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Product_PictureUrl",
                table: "OrderItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
