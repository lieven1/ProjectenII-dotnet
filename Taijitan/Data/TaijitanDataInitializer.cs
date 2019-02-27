using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data {
    public class TaijitanDataInitializer {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TaijitanDataInitializer(ApplicationDbContext context, UserManager<IdentityUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        public async Task InitializeData() {
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated()) {
                await InitializeUsers();

                Gebruiker BruceLee = new Gebruiker("Lee", "Bruce", new DateTime(1940, 11, 27), "0479076258", "BruceLee@MartialArt.com", new Adres("België", "9820", "Gent", "MartialArtStraat", "5a"));

                _context.Gebruikers.Add(BruceLee);
                _context.SaveChanges();
            }
        }

        private async Task InitializeUsers() {
            string email = "taijitan@taijitan.be";
            IdentityUser user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, "P@ssword1");
            _context.SaveChanges();
        }

    }
}