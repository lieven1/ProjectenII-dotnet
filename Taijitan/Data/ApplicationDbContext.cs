using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taijitan.Data.Mappers;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Databindings;

namespace Taijitan.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Lesmoment> Lesmomenten { get; set; }
        public DbSet<Adres> Adressen { get; set; }
        public DbSet<LesmomentLeden> LesmomentLeden { get; set; }
        public DbSet<Thema> Themas { get; set; }
        public DbSet<Lesmateriaal> Lesmateriaal { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<FotoLesmateriaal> FotoLesmateriaal { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new GebruikerConfiguration());
            builder.ApplyConfiguration(new AdresConfiguration());
            builder.ApplyConfiguration(new LesmomentConfiguration());
            builder.ApplyConfiguration(new LesmomentLedenConfiguration());
            builder.ApplyConfiguration(new ThemaConfiguration());
            builder.ApplyConfiguration(new LesmateriaalConfiguration());
            builder.ApplyConfiguration(new FotoConfiguration());
            builder.ApplyConfiguration(new FotoLesmateriaalConfiguration());
        }
    }
}
