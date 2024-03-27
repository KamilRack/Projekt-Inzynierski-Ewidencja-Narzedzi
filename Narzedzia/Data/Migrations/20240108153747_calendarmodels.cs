using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    public partial class calendarmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    IdAwaria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionCal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartCal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndCal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NarzedzieId = table.Column<int>(type: "int", nullable: true),
                    StanowiskoId = table.Column<int>(type: "int", nullable: true),
                    WydzialId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.IdAwaria);
                    table.ForeignKey(
                        name: "FK_Events_Narzedzia_NarzedzieId",
                        column: x => x.NarzedzieId,
                        principalTable: "Narzedzia",
                        principalColumn: "NarzedzieId");
                    table.ForeignKey(
                        name: "FK_Events_Stanowiska_StanowiskoId",
                        column: x => x.StanowiskoId,
                        principalTable: "Stanowiska",
                        principalColumn: "StanowiskoId");
                    table.ForeignKey(
                        name: "FK_Events_Wydzialy_WydzialId",
                        column: x => x.WydzialId,
                        principalTable: "Wydzialy",
                        principalColumn: "WydzialId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_NarzedzieId",
                table: "Events",
                column: "NarzedzieId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_StanowiskoId",
                table: "Events",
                column: "StanowiskoId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_WydzialId",
                table: "Events",
                column: "WydzialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
