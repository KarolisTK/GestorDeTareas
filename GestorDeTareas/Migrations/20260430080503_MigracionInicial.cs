using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeTareas.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    IdTarea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTarea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescripcionTarea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacionTarea = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstadosTarea = table.Column<int>(type: "int", nullable: true),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: true),
                    TiposTarea = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioDeLaTarea = table.Column<int>(type: "int", nullable: false),
                    TienePrioridad = table.Column<bool>(type: "bit", nullable: true),
                    FechaLimite = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.IdTarea);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContrasenaUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
