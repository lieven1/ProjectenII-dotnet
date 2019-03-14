using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Databindings;
using Taijitan.Models.Domain.Enums;

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
                Adres adres2 = new Adres("België", "9820", "Gent", "Ledenstraat", "16");
                _context.Adressen.Add(adres1);
                _context.Adressen.Add(adres2);

                Gebruiker BruceLee = new Gebruiker("taijitan2", "11111111111", new DateTime(2018, 05, 16), "Lee", "Bruce", Geslacht.Man, new DateTime(1940, 11, 27), "UZ Gent", null, "0479076258", "BruceLee@MartialArt.com", "BruceLeesMom@MartialArt.com", adres1, 100, Gradatie.SanDan, TypeGebruiker.Lid);
                Gebruiker Lid = new Gebruiker("lid2", "12312312312", new DateTime(2018, 05, 24), "John", "Doe", Geslacht.Man, new DateTime(1960, 3, 24), "Brussel", "0525252525", "0479076258", "lid@MartialArt.com", "LidsMom@MartialArt.com", adres1, 100, Gradatie.RokkuKyu, TypeGebruiker.Lid);
                Gebruiker Lid2 = new Gebruiker("Doc", "12345678912", new DateTime(2018, 02, 12), "Johny", "Medicine", Geslacht.Man, new DateTime(1970, 3, 24), "Antwerpen", "0525252526", "0479076259", "doc@MartialArt.com", "docsMom@MartialArt.com", adres2, 101, Gradatie.ShichiDan, TypeGebruiker.Lid);
                _context.Gebruikers.Add(BruceLee);
                _context.Gebruikers.Add(Lid);
                _context.Gebruikers.Add(Lid2);
                _context.SaveChanges();

                DateTime datum = DateTime.Now;
                Lesmoment lesmoment1 = new Lesmoment(datum, datum.AddDays(1), datum.AddDays(1).AddHours(2));
                Lesmoment lesmoment2 = new Lesmoment(datum, datum.AddDays(2), datum.AddDays(2).AddHours(2));
                Lesmoment lesmoment3 = new Lesmoment(datum, datum.AddDays(3), datum.AddDays(3).AddHours(2));
                Lesmoment lesmoment4 = new Lesmoment(datum, datum.AddSeconds(30), datum.AddSeconds(60));

                LesmomentLeden lesmoment1LedenBruceLee = new LesmomentLeden(lesmoment1, BruceLee, true);
                LesmomentLeden lesmoment1LedenLid = new LesmomentLeden(lesmoment1, Lid, true);
                LesmomentLeden lesmoment2LedenBruceLee = new LesmomentLeden(lesmoment2, BruceLee, true);
                LesmomentLeden lesmoment2LedenLid = new LesmomentLeden(lesmoment2, Lid, true);
                LesmomentLeden lesmoment3LedenBruceLee = new LesmomentLeden(lesmoment3, BruceLee, true);
                LesmomentLeden lesmoment3LedenLid = new LesmomentLeden(lesmoment3, Lid, true);
                LesmomentLeden lesmoment4LedenBruceLee = new LesmomentLeden(lesmoment4, BruceLee, true);
                LesmomentLeden lesmoment4LedenLid = new LesmomentLeden(lesmoment4, Lid, true);

                List<LesmomentLeden> lesmomentLeden1 = new List<LesmomentLeden>();
                List<LesmomentLeden> lesmomentLeden2 = new List<LesmomentLeden>();
                List<LesmomentLeden> lesmomentLeden3 = new List<LesmomentLeden>();
                List<LesmomentLeden> lesmomentLeden4 = new List<LesmomentLeden>();

                lesmomentLeden1.Add(lesmoment1LedenBruceLee);
                lesmomentLeden1.Add(lesmoment1LedenLid);
                lesmomentLeden2.Add(lesmoment2LedenBruceLee);
                lesmomentLeden2.Add(lesmoment2LedenLid);
                lesmomentLeden3.Add(lesmoment3LedenBruceLee);
                lesmomentLeden3.Add(lesmoment3LedenLid);
                lesmomentLeden4.Add(lesmoment4LedenBruceLee);
                lesmomentLeden4.Add(lesmoment4LedenLid);

                lesmoment1.Leden = lesmomentLeden1;
                lesmoment2.Leden = lesmomentLeden2;
                lesmoment3.Leden = lesmomentLeden3;
                lesmoment4.Leden = lesmomentLeden4;


                _context.Lesmomenten.Add(lesmoment1);
                _context.Lesmomenten.Add(lesmoment2);
                _context.Lesmomenten.Add(lesmoment3);
                _context.Lesmomenten.Add(lesmoment4);

                _context.SaveChanges();

                await InitializeUsers();
            }
        }

        private async Task InitializeUsers()
        {
            await InitializeLid();
            await InitializeBeheerder();
        }

        private async Task InitializeLid()
        {
            string email = "lid@taijitan.be";
            string usr = "lid";
            IdentityUser user = new IdentityUser { UserName = usr, Email = email };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "gebruiker"));
            Adres adres1 = new Adres("België", "9820", "Gent", "Ledenstraat", "16");
            var gebruiker = new Gebruiker(usr, "11111545645", new DateTime(2018, 05, 24), "John", "Doe", Geslacht.Man, new DateTime(1960, 3, 24), "Brussel", "0525252525", "0479076258", "lid@MartialArt.com", "LidsMom@MartialArt.com", adres1, 100, Gradatie.ShoDan, TypeGebruiker.Lid);
            _context.Gebruikers.Add(gebruiker);
            _context.SaveChanges();
        }
        private async Task InitializeBeheerder()
        {
            string email = "taijitan@taijitan.be";
            string usr = "taijitan";
            IdentityUser user = new IdentityUser { UserName = usr, Email = email };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "beheerder"));


            Adres adres1 = new Adres("België", "9820", "Gent", "MartialArtStraat", "5a");
            var gebruiker = new Gebruiker(usr, "11111111111", new DateTime(2018, 05, 16), "Lee", "Bruce", Geslacht.Man, new DateTime(1940, 11, 27), "UZ Gent", null, "0479076258", "BruceLee@MartialArt.com", "BruceLeesMom@MartialArt.com", adres1, 100, Gradatie.YonDan, TypeGebruiker.Beheerder);
            _context.Gebruikers.Add(gebruiker);
            _context.SaveChanges();
        }
    }
}
