using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    public partial class AwariaTable_Patch1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UzytkownikRealizujacyId",
                table: "Awarie",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Awarie_UzytkownikRealizujacyId",
                table: "Awarie",
                column: "UzytkownikRealizujacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Awarie_AspNetUsers_UzytkownikRealizujacyId",
                table: "Awarie",
                column: "UzytkownikRealizujacyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awarie_AspNetUsers_UzytkownikRealizujacyId",
                table: "Awarie");

            migrationBuilder.DropIndex(
                name: "IX_Awarie_UzytkownikRealizujacyId",
                table: "Awarie");

            migrationBuilder.DropColumn(
                name: "UzytkownikRealizujacyId",
                table: "Awarie");
        }
    }
}
