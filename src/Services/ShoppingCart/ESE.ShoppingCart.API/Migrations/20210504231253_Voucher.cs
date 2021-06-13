using Microsoft.EntityFrameworkCore.Migrations;

namespace ESE.ShoppingCart.API.Migrations
{
    public partial class Voucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "CustomerCarts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "VoucherUsed",
                table: "CustomerCarts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VoucherCode",
                table: "CustomerCarts",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiscountType",
                table: "CustomerCarts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "CustomerCarts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscount",
                table: "CustomerCarts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "CustomerCarts");

            migrationBuilder.DropColumn(
                name: "VoucherUsed",
                table: "CustomerCarts");

            migrationBuilder.DropColumn(
                name: "VoucherCode",
                table: "CustomerCarts");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                table: "CustomerCarts");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "CustomerCarts");

            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "CustomerCarts");
        }
    }
}
