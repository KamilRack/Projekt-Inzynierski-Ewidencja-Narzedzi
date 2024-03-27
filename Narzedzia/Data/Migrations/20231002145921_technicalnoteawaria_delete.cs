using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    public partial class technicalnoteawaria_delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotatkaTechniczna",
                table: "Awarie");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotatkaTechniczna",
                table: "Awarie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
