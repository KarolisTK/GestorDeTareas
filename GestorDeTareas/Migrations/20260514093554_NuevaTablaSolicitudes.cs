using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GestorDeTareas.Migrations
{
    /// <inheritdoc />
    public partial class NuevaTablaSolicitudes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    IdSolicitud = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEmisor = table.Column<int>(type: "integer", nullable: false),
                    EmisorIdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdReceptor = table.Column<int>(type: "integer", nullable: false),
                    ReceptorIdUsuario = table.Column<int>(type: "integer", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TiposEstado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.IdSolicitud);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Usuarios_EmisorIdUsuario",
                        column: x => x.EmisorIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Usuarios_ReceptorIdUsuario",
                        column: x => x.ReceptorIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_EmisorIdUsuario",
                table: "Solicitudes",
                column: "EmisorIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_ReceptorIdUsuario",
                table: "Solicitudes",
                column: "ReceptorIdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Solicitudes");
        }
    }
}
