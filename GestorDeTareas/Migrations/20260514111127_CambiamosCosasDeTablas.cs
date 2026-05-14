using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeTareas.Migrations
{
    /// <inheritdoc />
    public partial class CambiamosCosasDeTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEspacioDeTrabajoACompartir",
                table: "Solicitudes",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEspacioDeTrabajoACompartir",
                table: "Solicitudes");
        }
    }
}
