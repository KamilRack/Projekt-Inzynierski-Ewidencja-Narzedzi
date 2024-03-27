using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    public partial class testgraphic_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZdjecieBytes",
                table: "Narzedzia");

            migrationBuilder.AddColumn<string>(
                name: "ZdjecieFileName",
                table: "Narzedzia",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZdjecieFileName",
                table: "Narzedzia");

            migrationBuilder.AddColumn<byte[]>(
                name: "ZdjecieBytes",
                table: "Narzedzia",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
