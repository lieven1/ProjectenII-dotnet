using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taijitan.Models.Domain.Databindings;

namespace Taijitan.Data.Mappers
{
    public class LesmomentLedenConfiguration : IEntityTypeConfiguration<LesmomentLeden>
    {
        public void Configure(EntityTypeBuilder<LesmomentLeden> builder)
        {
            builder.ToTable("LesmomentLeden");
            builder.HasKey(t => new { t.LesmomentId, t.Gebruikersnaam });
            builder.HasOne(t => t.Lesmoment).WithMany(l => l.Leden).HasForeignKey(t => t.LesmomentId);
            builder.HasOne(t => t.Gebruiker).WithMany(l => l.Lesmomenten).HasForeignKey(t => t.Gebruikersnaam);
            builder.Property(t => t.Ingeschreven).IsRequired();
            builder.Property(t => t.Aanwezig).IsRequired();
            builder.HasOne(t => t.Formule).WithMany();
        }
    }
}
