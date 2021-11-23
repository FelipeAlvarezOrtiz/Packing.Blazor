using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Packing.Persistencia.Migrations
{
    public partial class CambiosPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_EstadosPedidos_EstadoIdEstadoPedido",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_EstadoIdEstadoPedido",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "EstadoIdEstadoPedido",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "EstadoIdEstadoPedido",
                table: "DetallePedidos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                table: "DetallePedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    GuidNotificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Severidad = table.Column<int>(type: "int", nullable: false),
                    MensajeNotificacion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Notificado = table.Column<bool>(type: "bit", nullable: false),
                    OrigenNotificacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.GuidNotificacion);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_EstadoIdEstadoPedido",
                table: "DetallePedidos",
                column: "EstadoIdEstadoPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_EstadosPedidos_EstadoIdEstadoPedido",
                table: "DetallePedidos",
                column: "EstadoIdEstadoPedido",
                principalTable: "EstadosPedidos",
                principalColumn: "IdEstadoPedido",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_EstadosPedidos_EstadoIdEstadoPedido",
                table: "DetallePedidos");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_EstadoIdEstadoPedido",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "EstadoIdEstadoPedido",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                table: "DetallePedidos");

            migrationBuilder.AddColumn<int>(
                name: "EstadoIdEstadoPedido",
                table: "Pedidos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EstadoIdEstadoPedido",
                table: "Pedidos",
                column: "EstadoIdEstadoPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_EstadosPedidos_EstadoIdEstadoPedido",
                table: "Pedidos",
                column: "EstadoIdEstadoPedido",
                principalTable: "EstadosPedidos",
                principalColumn: "IdEstadoPedido",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
