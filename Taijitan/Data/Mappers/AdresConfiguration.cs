using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Mappers
{
    public class AdresConfiguration : IEntityTypeConfiguration<Adres>
    {
        public void Configure(EntityTypeBuilder<Adres> builder)
        {
            builder.ToTable("Adres");
            builder.HasKey(t => t.AdresId);
            builder.Property(t => t.Land).IsRequired();
            builder.Property(t => t.Nummer).IsRequired();
            builder.Property(t => t.Postcode).IsRequired();
            builder.Property(t => t.Stad).IsRequired();
            builder.Property(t => t.Straat).IsRequired();
        }
    }
}
