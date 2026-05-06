using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    IdTarea = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreTarea = table.Column<string>(type: "text", nullable: true),
                    DescripcionTarea = table.Column<string>(type: "text", nullable: true),
                    FechaCreacionTarea = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EstadosTarea = table.Column<int>(type: "integer", nullable: true),
                    EstaEliminado = table.Column<bool>(type: "boolean", nullable: true),
                    TiposTarea = table.Column<int>(type: "integer", nullable: false),
                    IdUsuarioDeLaTarea = table.Column<int>(type: "integer", nullable: false),
                    TienePrioridad = table.Column<bool>(type: "boolean", nullable: true),
                    FechaLimite = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.IdTarea);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreUsuario = table.Column<string>(type: "text", nullable: false),
                    CorreoUsuario = table.Column<string>(type: "text", nullable: false),
                    ContrasenaUsuario = table.Column<string>(type: "text", nullable: false),
                    EstaEliminado = table.Column<bool>(type: "boolean", nullable: true)
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
