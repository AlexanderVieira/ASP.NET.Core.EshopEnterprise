using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESE.ShoppingCart.API.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    TotalValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", nullable: true),
                    CustomerCartId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCarts_CustomerCarts_CustomerCartId",
                        column: x => x.CustomerCartId,
                        principalTable: "CustomerCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_Customer",
                table: "CustomerCarts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCarts_CustomerCartId",
                table: "ItemCarts",
                column: "CustomerCartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCarts");

            migrationBuilder.DropTable(
                name: "CustomerCarts");
        }
    }
}
