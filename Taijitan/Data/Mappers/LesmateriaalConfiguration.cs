using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers {
    public class LesmateriaalConfiguration : IEntityTypeConfiguration<Lesmateriaal> {
            public void Configure(EntityTypeBuilder<Lesmateriaal> builder) {
                builder.ToTable("Lesmateriaal");
                builder.HasKey(l => l.LesmateriaalId);
                builder.Property(t => t.Type).IsRequired();
                builder.Property(t => t.Naam).IsRequired();
                builder.Property(t => t.Graad).IsRequired();
            }
        }
}
