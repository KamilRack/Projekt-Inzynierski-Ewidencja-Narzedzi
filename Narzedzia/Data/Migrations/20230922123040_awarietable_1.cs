using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    public partial class awarietable_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Awarie",
                columns: table => new
                {
                    IdAwaria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NarzedzieId = table.Column<int>(type: "int", nullable: false),
                    DescriptionAwaria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberAwaria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPrzyjecia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UzytkownikId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Awarie", x => x.IdAwaria);
                    table.ForeignKey(
                        name: "FK_Awarie_AspNetUsers_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Awarie_Narzedzia_NarzedzieId",
                        column: x => x.NarzedzieId,
                        principalTable: "Narzedzia",
                        principalColumn: "NarzedzieId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Awarie_NarzedzieId",
                table: "Awarie",
                column: "NarzedzieId");

            migrationBuilder.CreateIndex(
                name: "IX_Awarie_UzytkownikId",
                table: "Awarie",
                column: "UzytkownikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Awarie");
        }
    }
}
