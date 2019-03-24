using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taijitan.Models.Domain.Databindings;

namespace Taijitan.Data {
    internal class FotoLesmateriaalConfiguration : IEntityTypeConfiguration<FotoLesmateriaal> {
        public void Configure(EntityTypeBuilder<FotoLesmateriaal> builder) {
            builder.ToTable("FotoLesmateriaal");
            builder.HasKey(t => new { t.FotoId, t.LesmateriaalId });
            builder.HasOne(t => t.Lesmateriaal).WithMany(l => l.Fotos).HasForeignKey(t => t.LesmateriaalId);
            builder.HasOne(t => t.Foto).WithMany(l => l.lesmateriaal).HasForeignKey(t => t.FotoId);
        }
    }
}