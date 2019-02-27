using System;
using Microsoft.EntityFrameworkCore;
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
            return _context.Gebruikers.Include(g => g.Adres).OrderBy(g => g.Gebruikersnaam).ToList();
        }

        public Gebruiker GetById(int v)
        {
            return _context.Gebruikers.Include(g => g.Adres).FirstOrDefault(g => g.GebruikerID == v);
        }

        public void RemoveGebruiker(Gebruiker gebruiker)
        {
            _context.Gebruikers.Remove(gebruiker);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
