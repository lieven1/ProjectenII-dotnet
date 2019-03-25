using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Data.Mappers
{
    public class LesformuleConfiguration : IEntityTypeConfiguration<Lesformule>
    {
        public void Configure(EntityTypeBuilder<Lesformule> builder)
        {
            builder.ToTable("Lesformule");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.LesText).IsRequired();
            builder.Property(t => t.TitleText).IsRequired();
        }

    }
}
