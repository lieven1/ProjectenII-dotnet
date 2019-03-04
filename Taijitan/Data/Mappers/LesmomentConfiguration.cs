using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class LesmomentConfiguration : IEntityTypeConfiguration<Lesmoment>
    {
        public void Configure(EntityTypeBuilder<Lesmoment> builder)
        {
            builder.ToTable("Lesmoment");
            builder.HasKey(t => t.LesmomentId);
            builder.Property(t => t.StartTijd).IsRequired();
            builder.Property(t => t.EindTijd).IsRequired();
        }
    }
}
