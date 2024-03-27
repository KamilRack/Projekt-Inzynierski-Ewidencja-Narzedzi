using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    public partial class calendarmodels112 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UzytkownikId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_UzytkownikId",
                table: "Events",
                column: "UzytkownikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_UzytkownikId",
                table: "Events",
                column: "UzytkownikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_UzytkownikId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_UzytkownikId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UzytkownikId",
                table: "Events");
        }
    }
}
