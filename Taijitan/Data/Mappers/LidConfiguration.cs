using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class LidConfiguration : IEntityTypeConfiguration<Gebruiker>
    {
        public void Configure(EntityTypeBuilder<Gebruiker> builder)
        {
            builder.ToTable("Gebruiker");
            builder.HasKey(t => t.Gebruikersnaam);
            builder.Property(t => t.Naam).IsRequired();
            builder.Property(t => t.Voornaam).IsRequired();
            builder.Property(t => t.Telefoonnummer).IsRequired();
            builder.Property(t => t.Geboortedatum).IsRequired();
            builder.Property(t => t.Email).IsRequired();

            builder.HasOne(t => t.Adres).WithOne().IsRequired();
        }
    }
}
