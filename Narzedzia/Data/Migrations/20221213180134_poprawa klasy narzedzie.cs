using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    public partial class poprawaklasynarzedzie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narzedzia_AspNetUsers_Id",
                table: "Narzedzia");

            migrationBuilder.DropIndex(
                name: "IX_Narzedzia_Id",
                table: "Narzedzia");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Narzedzia");

            migrationBuilder.AlterColumn<string>(
                name: "UzytkownikId",
                table: "Narzedzia",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Narzedzia_UzytkownikId",
                table: "Narzedzia",
                column: "UzytkownikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Narzedzia_AspNetUsers_UzytkownikId",
                table: "Narzedzia",
                column: "UzytkownikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narzedzia_AspNetUsers_UzytkownikId",
                table: "Narzedzia");

            migrationBuilder.DropIndex(
                name: "IX_Narzedzia_UzytkownikId",
                table: "Narzedzia");

            migrationBuilder.AlterColumn<int>(
                name: "UzytkownikId",
                table: "Narzedzia",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Narzedzia",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Narzedzia_Id",
                table: "Narzedzia",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Narzedzia_AspNetUsers_Id",
                table: "Narzedzia",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
