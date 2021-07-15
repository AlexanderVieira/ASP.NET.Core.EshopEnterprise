using Microsoft.EntityFrameworkCore.Migrations;

namespace ESE.Order.Infra.Migrations
{
    public partial class mappingOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItens_Pedidos_OrderId",
                table: "PedidoItens");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Vouchers_VoucherId",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoItens",
                table: "PedidoItens");

            migrationBuilder.RenameTable(
                name: "Pedidos",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "PedidoItens",
                newName: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "Logradouro",
                table: "Orders",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Orders",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Orders",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "Bairro",
                table: "Orders",
                newName: "District");

            migrationBuilder.RenameColumn(
                name: "Complemento",
                table: "Orders",
                newName: "Complement");

            migrationBuilder.RenameColumn(
                name: "Cep",
                table: "Orders",
                newName: "CodePostal");

            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "Orders",
                newName: "City");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_VoucherId",
                table: "Orders",
                newName: "IX_Orders_VoucherId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoItens_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Vouchers_VoucherId",
                table: "Orders",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Vouchers_VoucherId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Pedidos");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "PedidoItens");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Pedidos",
                newName: "Logradouro");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Pedidos",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Pedidos",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Pedidos",
                newName: "Bairro");

            migrationBuilder.RenameColumn(
                name: "Complement",
                table: "Pedidos",
                newName: "Complemento");

            migrationBuilder.RenameColumn(
                name: "CodePostal",
                table: "Pedidos",
                newName: "Cep");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Pedidos",
                newName: "Cidade");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_VoucherId",
                table: "Pedidos",
                newName: "IX_Pedidos_VoucherId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "PedidoItens",
                newName: "IX_PedidoItens_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoItens",
                table: "PedidoItens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItens_Pedidos_OrderId",
                table: "PedidoItens",
                column: "OrderId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Vouchers_VoucherId",
                table: "Pedidos",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
