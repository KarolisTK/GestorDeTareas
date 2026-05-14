using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeTareas.Migrations
{
    /// <inheritdoc />
    public partial class FixFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Usuarios_EmisorIdUsuario",
                table: "Solicitudes");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Usuarios_ReceptorIdUsuario",
                table: "Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_EmisorIdUsuario",
                table: "Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_ReceptorIdUsuario",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "EmisorIdUsuario",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "ReceptorIdUsuario",
                table: "Solicitudes");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_IdEmisor",
                table: "Solicitudes",
                column: "IdEmisor");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_IdReceptor",
                table: "Solicitudes",
                column: "IdReceptor");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Usuarios_IdEmisor",
                table: "Solicitudes",
                column: "IdEmisor",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Usuarios_IdReceptor",
                table: "Solicitudes",
                column: "IdReceptor",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Usuarios_IdEmisor",
                table: "Solicitudes");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Usuarios_IdReceptor",
                table: "Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_IdEmisor",
                table: "Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_IdReceptor",
                table: "Solicitudes");

            migrationBuilder.AddColumn<int>(
                name: "EmisorIdUsuario",
                table: "Solicitudes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReceptorIdUsuario",
                table: "Solicitudes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_EmisorIdUsuario",
                table: "Solicitudes",
                column: "EmisorIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_ReceptorIdUsuario",
                table: "Solicitudes",
                column: "ReceptorIdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Usuarios_EmisorIdUsuario",
                table: "Solicitudes",
                column: "EmisorIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Usuarios_ReceptorIdUsuario",
                table: "Solicitudes",
                column: "ReceptorIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
