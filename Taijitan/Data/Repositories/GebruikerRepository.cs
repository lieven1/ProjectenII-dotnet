using Microsoft.EntityFrameworkCore;
using System.Linq;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private readonly DbSet<Gebruiker> _gebruikers;
        private readonly ApplicationDbContext _context;

        public GebruikerRepository(ApplicationDbContext context)
        {
            _context = context;
            _gebruikers = _context.Gebruikers;
        }
        public Gebruiker GetBy(string gebruikersnaam)
        {
            var result = _gebruikers.Include(g => g.Adres).SingleOrDefault(g => g.Gebruikersnaam == gebruikersnaam);
            return result;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
