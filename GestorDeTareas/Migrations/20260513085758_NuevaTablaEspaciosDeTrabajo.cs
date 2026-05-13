using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GestorDeTareas.Migrations
{
    /// <inheritdoc />
    public partial class NuevaTablaEspaciosDeTrabajo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EspacioDeTrabajoId",
                table: "Tareas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EspaciosDeTrabajo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspaciosDeTrabajo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EspaciosDeTrabajoUsuario",
                columns: table => new
                {
                    EspaciosDeTrabajoId = table.Column<int>(type: "integer", nullable: false),
                    UsuariosIdUsuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspaciosDeTrabajoUsuario", x => new { x.EspaciosDeTrabajoId, x.UsuariosIdUsuario });
                    table.ForeignKey(
                        name: "FK_EspaciosDeTrabajoUsuario_EspaciosDeTrabajo_EspaciosDeTrabaj~",
                        column: x => x.EspaciosDeTrabajoId,
                        principalTable: "EspaciosDeTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EspaciosDeTrabajoUsuario_Usuarios_UsuariosIdUsuario",
                        column: x => x.UsuariosIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_EspacioDeTrabajoId",
                table: "Tareas",
                column: "EspacioDeTrabajoId");

            migrationBuilder.CreateIndex(
                name: "IX_EspaciosDeTrabajoUsuario_UsuariosIdUsuario",
                table: "EspaciosDeTrabajoUsuario",
                column: "UsuariosIdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_EspaciosDeTrabajo_EspacioDeTrabajoId",
                table: "Tareas",
                column: "EspacioDeTrabajoId",
                principalTable: "EspaciosDeTrabajo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_EspaciosDeTrabajo_EspacioDeTrabajoId",
                table: "Tareas");

            migrationBuilder.DropTable(
                name: "EspaciosDeTrabajoUsuario");

            migrationBuilder.DropTable(
                name: "EspaciosDeTrabajo");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_EspacioDeTrabajoId",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "EspacioDeTrabajoId",
                table: "Tareas");
        }
    }
}
