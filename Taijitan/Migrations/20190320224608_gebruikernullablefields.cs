using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Taijitan.Migrations
{
    public partial class gebruikernullablefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gebruiker_Adres_AdresId",
                table: "Gebruiker");

            migrationBuilder.DropForeignKey(
                name: "FK_Gebruiker_Gradatie_GradatieId",
                table: "Gebruiker");

            migrationBuilder.DropTable(
                name: "Gradatie");

            migrationBuilder.DropIndex(
                name: "IX_Gebruiker_GradatieId",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "Datum",
                table: "Lesmoment");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "GradatieId",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Gebruiker");

            migrationBuilder.AddColumn<int>(
                name: "Formule",
                table: "LesmomentLeden",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Actief",
                table: "Lesmoment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Rijksregisternummer",
                table: "Gebruiker",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Gsmnummer",
                table: "Gebruiker",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Geboorteplaats",
                table: "Gebruiker",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "AdresId",
                table: "Gebruiker",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Gradatie",
                table: "Gebruiker",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Lesformule",
                table: "Gebruiker",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Thema",
                columns: table => new
                {
                    ThemaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naam = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thema", x => x.ThemaId);
                });

            migrationBuilder.CreateTable(
                name: "Lesmateriaal",
                columns: table => new
                {
                    LesmateriaalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naam = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Graad = table.Column<int>(nullable: false),
                    ThemaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesmateriaal", x => x.LesmateriaalId);
                    table.ForeignKey(
                        name: "FK_Lesmateriaal_Thema_ThemaId",
                        column: x => x.ThemaId,
                        principalTable: "Thema",
                        principalColumn: "ThemaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lesmateriaal_ThemaId",
                table: "Lesmateriaal",
                column: "ThemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gebruiker_Adres_AdresId",
                table: "Gebruiker",
                column: "AdresId",
                principalTable: "Adres",
                principalColumn: "AdresId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gebruiker_Adres_AdresId",
                table: "Gebruiker");

            migrationBuilder.DropTable(
                name: "Lesmateriaal");

            migrationBuilder.DropTable(
                name: "Thema");

            migrationBuilder.DropColumn(
                name: "Formule",
                table: "LesmomentLeden");

            migrationBuilder.DropColumn(
                name: "Actief",
                table: "Lesmoment");

            migrationBuilder.DropColumn(
                name: "Gradatie",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "Lesformule",
                table: "Gebruiker");

            migrationBuilder.AddColumn<DateTime>(
                name: "Datum",
                table: "Lesmoment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Rijksregisternummer",
                table: "Gebruiker",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gsmnummer",
                table: "Gebruiker",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Geboorteplaats",
                table: "Gebruiker",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdresId",
                table: "Gebruiker",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Gebruiker",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Gebruiker",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Gebruiker",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "GradatieId",
                table: "Gebruiker",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Gebruiker",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Gebruiker",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Gebruiker",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Gebruiker",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Gebruiker",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Gebruiker",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Gebruiker",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Gebruiker",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Gebruiker",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Gebruiker",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Gebruiker",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gradatie",
                columns: table => new
                {
                    GradatieId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Graadnummer = table.Column<int>(nullable: false),
                    Naam = table.Column<string>(nullable: true),
                    Onderverdeling = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradatie", x => x.GradatieId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gebruiker_GradatieId",
                table: "Gebruiker",
                column: "GradatieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gebruiker_Adres_AdresId",
                table: "Gebruiker",
                column: "AdresId",
                principalTable: "Adres",
                principalColumn: "AdresId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gebruiker_Gradatie_GradatieId",
                table: "Gebruiker",
                column: "GradatieId",
                principalTable: "Gradatie",
                principalColumn: "GradatieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
