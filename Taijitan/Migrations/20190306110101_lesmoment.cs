using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Taijitan.Migrations
{
    public partial class lesmoment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gebruiker_Sessie_SessieStartTijd",
                table: "Gebruiker");

            migrationBuilder.DropTable(
                name: "Sessie");

            migrationBuilder.DropIndex(
                name: "IX_Gebruiker_SessieStartTijd",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "SessieStartTijd",
                table: "Gebruiker");

            migrationBuilder.AlterColumn<string>(
                name: "Telefoonnummer",
                table: "Gebruiker",
                nullable: true,
                oldClrType: typeof(string));

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

            migrationBuilder.AddColumn<string>(
                name: "EmailOuders",
                table: "Gebruiker",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Geboorteplaats",
                table: "Gebruiker",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Geslacht",
                table: "Gebruiker",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GradatieId",
                table: "Gebruiker",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gsmnummer",
                table: "Gebruiker",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Gebruiker",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Inschrijvingsdatum",
                table: "Gebruiker",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.AddColumn<int>(
                name: "Punten",
                table: "Gebruiker",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Rijksregisternummer",
                table: "Gebruiker",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Gebruiker",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Gebruiker",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TypeGebruiker",
                table: "Gebruiker",
                nullable: false,
                defaultValue: 0);

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
                    Onderverdeling = table.Column<string>(nullable: true),
                    Naam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradatie", x => x.GradatieId);
                });

            migrationBuilder.CreateTable(
                name: "Lesmoment",
                columns: table => new
                {
                    LesmomentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    StartTijd = table.Column<DateTime>(nullable: false),
                    EindTijd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesmoment", x => x.LesmomentId);
                });

            migrationBuilder.CreateTable(
                name: "LesmomentLeden",
                columns: table => new
                {
                    LesmomentId = table.Column<int>(nullable: false),
                    Gebruikersnaam = table.Column<string>(nullable: false),
                    Ingeschreven = table.Column<bool>(nullable: false),
                    Aanwezig = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LesmomentLeden", x => new { x.LesmomentId, x.Gebruikersnaam });
                    table.ForeignKey(
                        name: "FK_LesmomentLeden_Gebruiker_Gebruikersnaam",
                        column: x => x.Gebruikersnaam,
                        principalTable: "Gebruiker",
                        principalColumn: "Gebruikersnaam",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LesmomentLeden_Lesmoment_LesmomentId",
                        column: x => x.LesmomentId,
                        principalTable: "Lesmoment",
                        principalColumn: "LesmomentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gebruiker_GradatieId",
                table: "Gebruiker",
                column: "GradatieId");

            migrationBuilder.CreateIndex(
                name: "IX_LesmomentLeden_Gebruikersnaam",
                table: "LesmomentLeden",
                column: "Gebruikersnaam");

            migrationBuilder.AddForeignKey(
                name: "FK_Gebruiker_Gradatie_GradatieId",
                table: "Gebruiker",
                column: "GradatieId",
                principalTable: "Gradatie",
                principalColumn: "GradatieId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gebruiker_Gradatie_GradatieId",
                table: "Gebruiker");

            migrationBuilder.DropTable(
                name: "Gradatie");

            migrationBuilder.DropTable(
                name: "LesmomentLeden");

            migrationBuilder.DropTable(
                name: "Lesmoment");

            migrationBuilder.DropIndex(
                name: "IX_Gebruiker_GradatieId",
                table: "Gebruiker");

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
                name: "EmailOuders",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "Geboorteplaats",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "Geslacht",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "GradatieId",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "Gsmnummer",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "Inschrijvingsdatum",
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
                name: "Punten",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "Rijksregisternummer",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "TypeGebruiker",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Gebruiker");

            migrationBuilder.AlterColumn<string>(
                name: "Telefoonnummer",
                table: "Gebruiker",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SessieStartTijd",
                table: "Gebruiker",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sessie",
                columns: table => new
                {
                    StartTijd = table.Column<DateTime>(nullable: false),
                    EindTijd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessie", x => x.StartTijd);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gebruiker_SessieStartTijd",
                table: "Gebruiker",
                column: "SessieStartTijd");

            migrationBuilder.AddForeignKey(
                name: "FK_Gebruiker_Sessie_SessieStartTijd",
                table: "Gebruiker",
                column: "SessieStartTijd",
                principalTable: "Sessie",
                principalColumn: "StartTijd",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
