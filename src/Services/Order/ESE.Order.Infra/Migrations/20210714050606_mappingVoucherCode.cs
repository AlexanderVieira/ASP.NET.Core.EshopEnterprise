using Microsoft.EntityFrameworkCore.Migrations;

namespace ESE.Order.Infra.Migrations
{
    public partial class mappingVoucherCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Vouchers",
                newName: "VoucherCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VoucherCode",
                table: "Vouchers",
                newName: "Code");
        }
    }
}
