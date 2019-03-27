using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taijitan.Models.Domain;

namespace Taijitan.Data {
    public class RaadplegingConfiguration : IEntityTypeConfiguration<Raadpleging> {
        public void Configure(EntityTypeBuilder<Raadpleging> builder) {
            builder.ToTable("Raadpleging");
            builder.HasKey(r => r.RaadplegingId);
            //builder.HasAlternateKey(r => new { r.Gebruikersnaam, r.LesmateriaalId, r.Tijdstip });
            builder.HasOne(r => r.Gebruiker).WithMany(g => g.Raadplegingen).IsRequired();
            builder.HasOne(r => r.Lesmateriaal).WithMany(l => l.Raadplegingen).IsRequired();
            builder.Property(t => t.Tijdstip).IsRequired();
        }
    }
}