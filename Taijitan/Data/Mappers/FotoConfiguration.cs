using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taijitan.Models.Domain;

namespace Taijitan.Data {
    public class FotoConfiguration : IEntityTypeConfiguration<Foto> {
        public void Configure(EntityTypeBuilder<Foto> builder) {
            builder.ToTable("Foto");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Bestandsnaam).IsRequired();
            builder.Property(t => t.Extensie).IsRequired();
        }
    }
}