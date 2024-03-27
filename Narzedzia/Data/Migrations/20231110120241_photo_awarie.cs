using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    public partial class photo_awarie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notatka");

            migrationBuilder.AddColumn<string>(
                name: "ZdjecieFileName",
                table: "Awarie",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZdjecieFileName",
                table: "Awarie");

            migrationBuilder.CreateTable(
                name: "Notatka",
                columns: table => new
                {
                    IdNotatki = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AwariaIdAwaria = table.Column<int>(type: "int", nullable: true),
                    DataDodania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tresc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notatka", x => x.IdNotatki);
                    table.ForeignKey(
                        name: "FK_Notatka_Awarie_AwariaIdAwaria",
                        column: x => x.AwariaIdAwaria,
                        principalTable: "Awarie",
                        principalColumn: "IdAwaria");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notatka_AwariaIdAwaria",
                table: "Notatka",
                column: "AwariaIdAwaria");
        }
    }
}
