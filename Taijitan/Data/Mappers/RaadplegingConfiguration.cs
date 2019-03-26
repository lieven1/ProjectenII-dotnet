using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taijitan.Models.Domain;

namespace Taijitan.Data {
    public class RaadplegingConfiguration : IEntityTypeConfiguration<Raadpleging> {
        public void Configure(EntityTypeBuilder<Raadpleging> builder) {
            builder.ToTable("Raadpleging");
            builder.HasKey(r => r.RaadplegingId);
            builder.Property(t => t.Gebruikersnaam).IsRequired();
            builder.Property(t => t.LesmateriaalId).IsRequired();
            builder.Property(t => t.Tijdstip).IsRequired();
        }
    }
}