using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{

    public class GebruikerRepository : IGebruikerRepository
    {

        private readonly ApplicationDbContext _context;

        public GebruikerRepository(ApplicationDbContext context)
        {
            _context = context; //injectie
        }

        public void AddGebruiker(Gebruiker gebruiker)
        {
            _context.Add(gebruiker);
        }

        public IEnumerable<Gebruiker> getAll()
        {
            return _context.Gebruikers.OrderBy(g => g.Gebruikersnaam).ToList();
        }

        public Gebruiker GetById(int v)
        {
            return _context.Gebruikers.FirstOrDefault(g => g.gebruikerID == v);
        }

        public void RemoveGebruiker(Gebruiker gebruiker)
        {
            _context.Remove(GetById(gebruiker.gebruikerID));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
