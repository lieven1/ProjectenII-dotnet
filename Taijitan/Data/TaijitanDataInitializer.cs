using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;

namespace Taijitan.Data
{
    public class TaijitanDataInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TaijitanDataInitializer(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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

                Gebruiker BruceLee = new Gebruiker("taijitan2", "11111111112", new DateTime(2018, 05, 16),"Lee", "Bruce", Geslacht.Man, new DateTime(1940, 11, 27), "UZ Gent", null, "0479076258", "BruceLee@MartialArt.com", "BruceLeesMom@MartialArt.com", adres1, 100, new Gradatie(1, "Kyo", "Sho-kyo"), TypeGebruiker.Lid);
                Gebruiker Lid = new Gebruiker("lid2", "11111545645", new DateTime(2018, 05, 24), "John", "Doe", Geslacht.Man, new DateTime(1960, 3, 24), "Brussel", "0525252525", "0479076258", "lid@MartialArt.com", "LidsMom@MartialArt.com", adres1, 100, new Gradatie(1, "Kyo", "Sho-Dan"), TypeGebruiker.Lid);
                _context.Gebruikers.Add(BruceLee);
                _context.Gebruikers.Add(Lid);
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
            //await _roleManager.CreateAsync(new IdentityRole("Lid"));

            string email = "lid@taijitan.be";
            string usr = "lid";
            IdentityUser user = new IdentityUser { UserName = usr, Email = email };
            await _userManager.CreateAsync(user, "P@ssword1");
            //await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(usr), "Lid");

            Adres adres1 = new Adres("België", "9820", "Gent", "Ledenstraat", "16");
            var gebruiker = new Gebruiker(usr, "11111545645", new DateTime(2018, 05, 24), "John", "Doe", Geslacht.Man, new DateTime(1960, 3, 24), "Brussel", "0525252525", "0479076258", "lid@MartialArt.com", "LidsMom@MartialArt.com", adres1, 100, new Gradatie(1, "Kyo", "Sho-Dan"), TypeGebruiker.Lid);
            _context.Gebruikers.Add(gebruiker);
            _context.SaveChanges();
        }
        private async Task InitializeBeheerder()
        {
            //await _roleManager.CreateAsync(new IdentityRole("Beheerder"));

            string email = "taijitan@taijitan.be";
            string usr = "taijitan";
            IdentityUser user = new IdentityUser { UserName = usr, Email = email };
            await _userManager.CreateAsync(user, "P@ssword1");
            //await _userManager.AddToRoleAsync(user, "Beheerder");

            Adres adres1 = new Adres("België", "9820", "Gent", "MartialArtStraat", "5a");
            var gebruiker = new Gebruiker(usr, "11111111111", new DateTime(2018, 05, 16), "Lee", "Bruce", Geslacht.Man, new DateTime(1940, 11, 27), "UZ Gent", null, "0479076258", "BruceLee@MartialArt.com", "BruceLeesMom@MartialArt.com", adres1, 100, new Gradatie(1, "Kyo", "Sho-kyo"), TypeGebruiker.Beheerder);
            _context.Gebruikers.Add(gebruiker);
            _context.SaveChanges();
        }
    }
}
