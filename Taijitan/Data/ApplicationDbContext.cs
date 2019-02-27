using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taijitan.Models.Domain;

namespace Taijitan.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Gebruiker> Gebruikers { get; set; }
        //public DbSet<Sessie> Sessies { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }
     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionstring = @"Server=DESKTOP-KS6ATME;Database=Taijitan;Integrated Security=True;";
            optionsBuilder.UseSqlServer(connectionstring);
            //var connectionstring = @"Server=.\SQLEXPRESS;Database=Taijitan;Integrated Security=True;";
            //optionsBuilder.UseSqlServer(connectionstring);
        }
    }
}
