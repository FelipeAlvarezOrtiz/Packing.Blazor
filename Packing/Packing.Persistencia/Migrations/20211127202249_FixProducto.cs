using Microsoft.EntityFrameworkCore.Migrations;

namespace Packing.Persistencia.Migrations
{
    public partial class FixProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disponibilidades_Empresas_EmpresaIdEmpresa",
                table: "Disponibilidades");

            migrationBuilder.RenameColumn(
                name: "EmpresaIdEmpresa",
                table: "Disponibilidades",
                newName: "EmpresaCabeceraId");

            migrationBuilder.RenameIndex(
                name: "IX_Disponibilidades_EmpresaIdEmpresa",
                table: "Disponibilidades",
                newName: "IX_Disponibilidades_EmpresaCabeceraId");

            migrationBuilder.AlterColumn<string>(
                name: "NombreParaBusqueda",
                table: "Productos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Disponible",
                table: "Productos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Disponibilidades_Empresas_EmpresaCabeceraId",
                table: "Disponibilidades",
                column: "EmpresaCabeceraId",
                principalTable: "Empresas",
                principalColumn: "IdEmpresa",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disponibilidades_Empresas_EmpresaCabeceraId",
                table: "Disponibilidades");

            migrationBuilder.DropColumn(
                name: "Disponible",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "EmpresaCabeceraId",
                table: "Disponibilidades",
                newName: "EmpresaIdEmpresa");

            migrationBuilder.RenameIndex(
                name: "IX_Disponibilidades_EmpresaCabeceraId",
                table: "Disponibilidades",
                newName: "IX_Disponibilidades_EmpresaIdEmpresa");

            migrationBuilder.AlterColumn<string>(
                name: "NombreParaBusqueda",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Disponibilidades_Empresas_EmpresaIdEmpresa",
                table: "Disponibilidades",
                column: "EmpresaIdEmpresa",
                principalTable: "Empresas",
                principalColumn: "IdEmpresa",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
