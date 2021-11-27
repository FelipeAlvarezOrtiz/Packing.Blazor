using Microsoft.EntityFrameworkCore.Migrations;

namespace Packing.Persistencia.Migrations
{
    public partial class ReferenciasRelaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Pedidos_PedidoGuidPedido",
                table: "DetallePedidos");

            migrationBuilder.RenameColumn(
                name: "PedidoGuidPedido",
                table: "DetallePedidos",
                newName: "PedidoCabeceraGuidPedido");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePedidos_PedidoGuidPedido",
                table: "DetallePedidos",
                newName: "IX_DetallePedidos_PedidoCabeceraGuidPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Pedidos_PedidoCabeceraGuidPedido",
                table: "DetallePedidos",
                column: "PedidoCabeceraGuidPedido",
                principalTable: "Pedidos",
                principalColumn: "GuidPedido",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Pedidos_PedidoCabeceraGuidPedido",
                table: "DetallePedidos");

            migrationBuilder.RenameColumn(
                name: "PedidoCabeceraGuidPedido",
                table: "DetallePedidos",
                newName: "PedidoGuidPedido");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePedidos_PedidoCabeceraGuidPedido",
                table: "DetallePedidos",
                newName: "IX_DetallePedidos_PedidoGuidPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Pedidos_PedidoGuidPedido",
                table: "DetallePedidos",
                column: "PedidoGuidPedido",
                principalTable: "Pedidos",
                principalColumn: "GuidPedido",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
