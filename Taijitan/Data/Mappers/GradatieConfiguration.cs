using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class GradatieConfiguration : IEntityTypeConfiguration<Gradatie>
    {
        public void Configure(EntityTypeBuilder<Gradatie> builder)
        {
            builder.ToTable("Gradatie");
            builder.HasKey(t => t.GradatieId);
            builder.Property(t => t.Graadnummer).IsRequired();
            builder.Property(t => t.Naam).IsRequired();
            builder.Property(t => t.Onderverdeling).IsRequired();
        }
    }
}
