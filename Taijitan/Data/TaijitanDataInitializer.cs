using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;
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
                _context.Adressen.Add(adres1);
                _context.SaveChanges();

                Gebruiker BruceLee = new Gebruiker("taijitan2", "11111111112", new DateTime(2018, 05, 16),"Lee", "Bruce", Geslacht.Man, new DateTime(1940, 11, 27), "UZ Gent", null, "0479076258", "BruceLee@MartialArt.com", "BruceLeesMom@MartialArt.com", adres1, 100, new Gradatie(1, "Kyo", "Sho-kyo"), TypeGebruiker.Lid);
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

            Adres adres1 = new Adres("België", "9820", "Gent", "MartialArtStraat", "5a");
            var gebruiker = new Gebruiker(usr, "11111111111", new DateTime(2018, 05, 16), "Lee", "Bruce", Geslacht.Man, new DateTime(1940, 11, 27), "UZ Gent", null, "0479076258", "BruceLee@MartialArt.com", "BruceLeesMom@MartialArt.com", adres1, 100, new Gradatie(1, "Kyo", "Sho-kyo"), TypeGebruiker.Lid);
            _context.Gebruikers.Add(gebruiker);
            _context.SaveChanges();
        }
    }
}
