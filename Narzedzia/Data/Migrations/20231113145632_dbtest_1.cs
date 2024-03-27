using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    public partial class dbtest_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StanowiskoId",
                table: "Awarie",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WydzialId",
                table: "Awarie",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Awarie_StanowiskoId",
                table: "Awarie",
                column: "StanowiskoId");

            migrationBuilder.CreateIndex(
                name: "IX_Awarie_WydzialId",
                table: "Awarie",
                column: "WydzialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Awarie_Stanowiska_StanowiskoId",
                table: "Awarie",
                column: "StanowiskoId",
                principalTable: "Stanowiska",
                principalColumn: "StanowiskoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Awarie_Wydzialy_WydzialId",
                table: "Awarie",
                column: "WydzialId",
                principalTable: "Wydzialy",
                principalColumn: "WydzialId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awarie_Stanowiska_StanowiskoId",
                table: "Awarie");

            migrationBuilder.DropForeignKey(
                name: "FK_Awarie_Wydzialy_WydzialId",
                table: "Awarie");

            migrationBuilder.DropIndex(
                name: "IX_Awarie_StanowiskoId",
                table: "Awarie");

            migrationBuilder.DropIndex(
                name: "IX_Awarie_WydzialId",
                table: "Awarie");

            migrationBuilder.DropColumn(
                name: "StanowiskoId",
                table: "Awarie");

            migrationBuilder.DropColumn(
                name: "WydzialId",
                table: "Awarie");
        }
    }
}
