using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers {
    public class ThemaConfiguration : IEntityTypeConfiguration<Thema> {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Thema> builder) {
            builder.ToTable("Thema");
            builder.HasKey(t => t.ThemaId);
            builder.Property(t => t.Naam).IsRequired();
        }
    }
}
