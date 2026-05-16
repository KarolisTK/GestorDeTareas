using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GestorDeTareas.Migrations
{
    /// <inheritdoc />
    public partial class NuevaTablaNotificaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    IdNotificacion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEmisor = table.Column<int>(type: "integer", nullable: false),
                    IdReceptor = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacionNotificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoNotificacion = table.Column<int>(type: "integer", nullable: false),
                    TituloNotificacion = table.Column<string>(type: "text", nullable: false),
                    ContenidoNotificacion = table.Column<string>(type: "text", nullable: false),
                    MarcadoComoLeido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.IdNotificacion);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Usuarios_IdEmisor",
                        column: x => x.IdEmisor,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Usuarios_IdReceptor",
                        column: x => x.IdReceptor,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_IdEmisor",
                table: "Notificaciones",
                column: "IdEmisor");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_IdReceptor",
                table: "Notificaciones",
                column: "IdReceptor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notificaciones");
        }
    }
}
