using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeTareas.Migrations
{
    /// <inheritdoc />
    public partial class QuitarClavePrimariaCompuesta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Amigos",
                table: "Amigos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amigos",
                table: "Amigos",
                column: "IdAmigos");

            migrationBuilder.CreateIndex(
                name: "IX_Amigos_IdUsuario",
                table: "Amigos",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Amigos",
                table: "Amigos");

            migrationBuilder.DropIndex(
                name: "IX_Amigos_IdUsuario",
                table: "Amigos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amigos",
                table: "Amigos",
                columns: new[] { "IdUsuario", "IdUsuario2" });
        }
    }
}
