using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narzedzia.Data.Migrations
{
    public partial class nowe_tabele : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Imie",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nazwisko",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NrKontrolny",
                table: "AspNetUsers",
                type: "int",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StanowiskoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WydzialId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    KategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaKategorii = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.KategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Producenci",
                columns: table => new
                {
                    ProducentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaProducenta = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producenci", x => x.ProducentId);
                });

            migrationBuilder.CreateTable(
                name: "Stanowiska",
                columns: table => new
                {
                    StanowiskoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaStanowiska = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanowiska", x => x.StanowiskoId);
                });

            migrationBuilder.CreateTable(
                name: "Wydzialy",
                columns: table => new
                {
                    WydzialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaWydzialu = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wydzialy", x => x.WydzialId);
                });

            migrationBuilder.CreateTable(
                name: "Narzedzia",
                columns: table => new
                {
                    NarzedzieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProducentId = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    KategoriaId = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    DataPrzyjecia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UzytkownikId = table.Column<int>(type: "int", nullable: false),
                    NumerNarzedzia = table.Column<int>(type: "int", maxLength: 15, nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narzedzia", x => x.NarzedzieId);
                    table.ForeignKey(
                        name: "FK_Narzedzia_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Narzedzia_Kategorie_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "Kategorie",
                        principalColumn: "KategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Narzedzia_Producenci_ProducentId",
                        column: x => x.ProducentId,
                        principalTable: "Producenci",
                        principalColumn: "ProducentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StanowiskoId",
                table: "AspNetUsers",
                column: "StanowiskoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WydzialId",
                table: "AspNetUsers",
                column: "WydzialId");

            migrationBuilder.CreateIndex(
                name: "IX_Narzedzia_Id",
                table: "Narzedzia",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Narzedzia_KategoriaId",
                table: "Narzedzia",
                column: "KategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Narzedzia_ProducentId",
                table: "Narzedzia",
                column: "ProducentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Stanowiska_StanowiskoId",
                table: "AspNetUsers",
                column: "StanowiskoId",
                principalTable: "Stanowiska",
                principalColumn: "StanowiskoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wydzialy_WydzialId",
                table: "AspNetUsers",
                column: "WydzialId",
                principalTable: "Wydzialy",
                principalColumn: "WydzialId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Stanowiska_StanowiskoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wydzialy_WydzialId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Narzedzia");

            migrationBuilder.DropTable(
                name: "Stanowiska");

            migrationBuilder.DropTable(
                name: "Wydzialy");

            migrationBuilder.DropTable(
                name: "Kategorie");

            migrationBuilder.DropTable(
                name: "Producenci");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StanowiskoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WydzialId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Imie",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nazwisko",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NrKontrolny",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StanowiskoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WydzialId",
                table: "AspNetUsers");
        }
    }
}
