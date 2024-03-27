using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    public partial class _1231313 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Narzedzia_NarzedzieId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Stanowiska_StanowiskoId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Wydzialy_WydzialId",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Narzedzia_NarzedzieId",
                table: "Events",
                column: "NarzedzieId",
                principalTable: "Narzedzia",
                principalColumn: "NarzedzieId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Stanowiska_StanowiskoId",
                table: "Events",
                column: "StanowiskoId",
                principalTable: "Stanowiska",
                principalColumn: "StanowiskoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Wydzialy_WydzialId",
                table: "Events",
                column: "WydzialId",
                principalTable: "Wydzialy",
                principalColumn: "WydzialId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Narzedzia_NarzedzieId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Stanowiska_StanowiskoId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Wydzialy_WydzialId",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Narzedzia_NarzedzieId",
                table: "Events",
                column: "NarzedzieId",
                principalTable: "Narzedzia",
                principalColumn: "NarzedzieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Stanowiska_StanowiskoId",
                table: "Events",
                column: "StanowiskoId",
                principalTable: "Stanowiska",
                principalColumn: "StanowiskoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Wydzialy_WydzialId",
                table: "Events",
                column: "WydzialId",
                principalTable: "Wydzialy",
                principalColumn: "WydzialId");
        }
    }
}
