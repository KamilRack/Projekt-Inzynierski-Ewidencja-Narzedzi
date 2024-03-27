using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    public partial class tescik202432334346433 : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "WydzialId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StanowiskoId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NarzedzieId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Narzedzia_NarzedzieId",
                table: "Events",
                column: "NarzedzieId",
                principalTable: "Narzedzia",
                principalColumn: "NarzedzieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Stanowiska_StanowiskoId",
                table: "Events",
                column: "StanowiskoId",
                principalTable: "Stanowiska",
                principalColumn: "StanowiskoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Wydzialy_WydzialId",
                table: "Events",
                column: "WydzialId",
                principalTable: "Wydzialy",
                principalColumn: "WydzialId",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<int>(
                name: "WydzialId",
                table: "Events",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StanowiskoId",
                table: "Events",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NarzedzieId",
                table: "Events",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
