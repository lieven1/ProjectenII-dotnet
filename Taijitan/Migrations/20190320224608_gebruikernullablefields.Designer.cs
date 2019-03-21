﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taijitan.Data;

namespace Taijitan.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190320224608_gebruikernullablefields")]
    partial class gebruikernullablefields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Taijitan.Models.Domain.Adres", b =>
                {
                    b.Property<int>("AdresId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Land")
                        .IsRequired();

                    b.Property<string>("Nummer")
                        .IsRequired();

                    b.Property<string>("Postcode")
                        .IsRequired();

                    b.Property<string>("Stad")
                        .IsRequired();

                    b.Property<string>("Straat")
                        .IsRequired();

                    b.HasKey("AdresId");

                    b.ToTable("Adres");
                });

            modelBuilder.Entity("Taijitan.Models.Domain.Databindings.LesmomentLeden", b =>
                {
                    b.Property<int>("LesmomentId");

                    b.Property<string>("Gebruikersnaam");

                    b.Property<bool>("Aanwezig");

                    b.Property<int>("Formule");

                    b.Property<bool>("Ingeschreven");

                    b.HasKey("LesmomentId", "Gebruikersnaam");

                    b.HasIndex("Gebruikersnaam");

                    b.ToTable("LesmomentLeden");
                });

            modelBuilder.Entity("Taijitan.Models.Domain.Gebruiker", b =>
                {
                    b.Property<string>("Gebruikersnaam")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdresId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("EmailOuders");

                    b.Property<DateTime>("Geboortedatum");

                    b.Property<string>("Geboorteplaats");

                    b.Property<int>("Geslacht");

                    b.Property<int>("Gradatie");

                    b.Property<string>("Gsmnummer");

                    b.Property<DateTime>("Inschrijvingsdatum");

                    b.Property<int>("Lesformule");

                    b.Property<string>("Naam")
                        .IsRequired();

                    b.Property<int>("Punten");

                    b.Property<string>("Rijksregisternummer");

                    b.Property<string>("Telefoonnummer");

                    b.Property<int>("TypeGebruiker");

                    b.Property<string>("Voornaam")
                        .IsRequired();

                    b.HasKey("Gebruikersnaam");

                    b.HasIndex("AdresId");

                    b.ToTable("Gebruiker");
                });

            modelBuilder.Entity("Taijitan.Models.Domain.Lesmateriaal", b =>
                {
                    b.Property<int>("LesmateriaalId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Graad");

                    b.Property<string>("Naam")
                        .IsRequired();

                    b.Property<int?>("ThemaId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("LesmateriaalId");

                    b.HasIndex("ThemaId");

                    b.ToTable("Lesmateriaal");
                });

            modelBuilder.Entity("Taijitan.Models.Domain.Lesmoment", b =>
                {
                    b.Property<int>("LesmomentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Actief");

                    b.Property<DateTime>("EindTijd");

                    b.Property<DateTime>("StartTijd");

                    b.HasKey("LesmomentId");

                    b.ToTable("Lesmoment");
                });

            modelBuilder.Entity("Taijitan.Models.Domain.Thema", b =>
                {
                    b.Property<int>("ThemaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naam")
                        .IsRequired();

                    b.HasKey("ThemaId");

                    b.ToTable("Thema");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Taijitan.Models.Domain.Databindings.LesmomentLeden", b =>
                {
                    b.HasOne("Taijitan.Models.Domain.Gebruiker", "Gebruiker")
                        .WithMany("Lesmomenten")
                        .HasForeignKey("Gebruikersnaam")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Taijitan.Models.Domain.Lesmoment", "Lesmoment")
                        .WithMany("Leden")
                        .HasForeignKey("LesmomentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Taijitan.Models.Domain.Gebruiker", b =>
                {
                    b.HasOne("Taijitan.Models.Domain.Adres", "Adres")
                        .WithMany()
                        .HasForeignKey("AdresId");
                });

            modelBuilder.Entity("Taijitan.Models.Domain.Lesmateriaal", b =>
                {
                    b.HasOne("Taijitan.Models.Domain.Thema")
                        .WithMany("Lesmateriaal")
                        .HasForeignKey("ThemaId");
                });
#pragma warning restore 612, 618
        }
    }
}
