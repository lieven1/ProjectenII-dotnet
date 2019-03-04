using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Databindings;

namespace Taijitan.Data.Mappers
{
    public class LesmomentLedenConfiguration : IEntityTypeConfiguration<LesmomentLeden>
    {
        public void Configure(EntityTypeBuilder<LesmomentLeden> builder)
        {
            builder.ToTable("LesmomentLeden");
            builder.HasKey(t => t.Id);
            builder.HasOne(t => t.Lesmoment).WithMany(t => t.Leden).IsRequired();
            builder.HasOne(t => t.Gebruiker).WithMany(t => t.LesmomentLeden).IsRequired();
            builder.Property(t => t.Ingeschreven).IsRequired();
            builder.Property(t => t.Aanwezig).IsRequired();
        }
    }
}
