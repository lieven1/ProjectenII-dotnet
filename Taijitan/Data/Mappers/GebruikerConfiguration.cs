using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class GebruikerConfiguration : IEntityTypeConfiguration<Gebruiker>
    {
        public void Configure(EntityTypeBuilder<Gebruiker> builder)
        {
            builder.ToTable("Gebruiker");
            builder.HasKey(t => t.Gebruikersnaam);
            builder.Property(t => t.Rijksregisternummer);
            builder.Property(t => t.Inschrijvingsdatum).IsRequired();
            builder.Property(t => t.Naam).IsRequired();
            builder.Property(t => t.Voornaam).IsRequired();
            builder.Property(t => t.Geslacht);
            builder.Property(t => t.Geboortedatum);
            builder.Property(t => t.Geboorteplaats);
            builder.Property(t => t.Telefoonnummer);
            builder.Property(t => t.Gsmnummer);
            builder.Property(t => t.Email).IsRequired();
            builder.Property(t => t.EmailOuders);
            builder.Property(t => t.Punten);
            builder.Property(t => t.Gradatie);
            builder.Property(t => t.TypeGebruiker).IsRequired();
            builder.Property(t => t.Lesformule);
            builder.HasOne(t => t.Adres).WithMany();
        }
    }
}
