using Microsoft.EntityFrameworkCore.Migrations;

namespace Packing.Persistencia.Migrations
{
    public partial class VisibilidadEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmpresaActiva",
                table: "Empresas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmpresaActiva",
                table: "Empresas");
        }
    }
}
