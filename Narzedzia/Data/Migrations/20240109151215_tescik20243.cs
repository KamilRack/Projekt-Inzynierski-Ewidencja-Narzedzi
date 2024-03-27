using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    public partial class tescik20243 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NarzedzieId1",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StanowiskoId1",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WydzialId1",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_NarzedzieId1",
                table: "Events",
                column: "NarzedzieId1");

            migrationBuilder.CreateIndex(
                name: "IX_Events_StanowiskoId1",
                table: "Events",
                column: "StanowiskoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Events_WydzialId1",
                table: "Events",
                column: "WydzialId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Narzedzia_NarzedzieId1",
                table: "Events",
                column: "NarzedzieId1",
                principalTable: "Narzedzia",
                principalColumn: "NarzedzieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Stanowiska_StanowiskoId1",
                table: "Events",
                column: "StanowiskoId1",
                principalTable: "Stanowiska",
                principalColumn: "StanowiskoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Wydzialy_WydzialId1",
                table: "Events",
                column: "WydzialId1",
                principalTable: "Wydzialy",
                principalColumn: "WydzialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Narzedzia_NarzedzieId1",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Stanowiska_StanowiskoId1",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Wydzialy_WydzialId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_NarzedzieId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_StanowiskoId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_WydzialId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "NarzedzieId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StanowiskoId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "WydzialId1",
                table: "Events");
        }
    }
}
