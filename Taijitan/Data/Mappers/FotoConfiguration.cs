using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taijitan.Models.Domain;

namespace Taijitan.Data {
    public class FotoConfiguration : IEntityTypeConfiguration<Foto> {
        public void Configure(EntityTypeBuilder<Foto> builder) {
            builder.ToTable("Foto");
            builder.HasKey(t => t.id);
            builder.Property(t => t.bestandsnaam).IsRequired();
            builder.Property(t => t.extensie).IsRequired();
        }
    }
}