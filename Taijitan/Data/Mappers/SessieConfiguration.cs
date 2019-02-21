using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class SessieConfiguration : IEntityTypeConfiguration<Sessie>
    {
        public void Configure(EntityTypeBuilder<Sessie> builder)
        {
            builder.ToTable("Sessie");
            builder.Property(t => t.StartTijd).IsRequired();
            builder.Property(t => t.EindTijd).IsRequired();
            builder.HasMany<Lid>(t => t.IngeschrevenLeden).WithOne().IsRequired();
        }
    }
}
