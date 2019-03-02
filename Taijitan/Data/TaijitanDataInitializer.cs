﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data
{
    public class TaijitanDataInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TaijitanDataInitializer(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {
                Adres adres1 = new Adres("België", "9820", "Gent", "MartialArtStraat", "5a");
                _context.Adressen.Add(adres1);
                _context.SaveChanges();

                Gebruiker BruceLee = new Gebruiker("brucelee", "Lee", "Bruce", new DateTime(1940, 11, 27), "0479076258", "BruceLee@MartialArt.com", adres1);
                _context.Gebruikers.Add(BruceLee);
                _context.SaveChanges();

                await InitializeUsers();
            }
        }

        private async Task InitializeUsers()
        {
            string email = "taijitan@taijitan.be";
            string usr = "taijitan";
            IdentityUser user = new IdentityUser { UserName = usr, Email = email };
            await _userManager.CreateAsync(user, "P@ssword1");

            var gebruiker = new Gebruiker
            {
                Gebruikersnaam = usr,
                Naam = "gebruiker",
                Voornaam = "gebruiker",
                Geboortedatum = new DateTime(),
                Telefoonnummer = "04593029",
                Email = email,
                Adres = _context.Adressen.FirstOrDefault(a => a.AdresId == 1)
            };
            _context.Gebruikers.Add(gebruiker);
            _context.SaveChanges();
        }
    }
}
