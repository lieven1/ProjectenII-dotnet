using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taijitan.Data.Mappers;
using Taijitan.Models.Domain;

namespace Taijitan.Data {
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Sessie> Sessies { get; set; }
        public DbSet<Adres> Adressen { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new GebruikerConfiguration());
            builder.ApplyConfiguration(new AdresConfiguration());
            builder.ApplyConfiguration(new SessieConfiguration());
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connectionstring = @"Server=.\SQLEXPRESS;Database=Taijitan;Integrated Security=True;";
        //    optionsBuilder.UseSqlServer(connectionstring);
        //}
    }
}
