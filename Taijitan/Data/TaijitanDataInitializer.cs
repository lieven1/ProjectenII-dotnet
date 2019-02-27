using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data
{
    public class TaijitanDataInitializer
    {
        private readonly ApplicationDbContext _context;

        public TaijitanDataInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void initializeData()
        {
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {
                Gebruiker BruceLee = new Gebruiker("BruceLee", "Lee", "Bruce", new DateTime(1940, 11, 27), "0479076258", "BruceLee@MartialArt.com", new Adres("België", "9820", "Gent", "MartialArtStraat", "5a"));
               
                _context.Gebruikers.Add(BruceLee);
                _context.SaveChanges();
            }
        }
    }
}
