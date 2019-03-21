using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;

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

        public List<Gebruiker> GetAllLeden()
        {
            return this._gebruikers.Include(g => g.Adres).Where(g => g.TypeGebruiker.Equals(TypeGebruiker.Lid)).ToList();
        }

        public List<Gebruiker> GetAllLedenInFormule(Lesformule formule)
        {
            return this._gebruikers.Include(g => g.Adres).Where(g => g.TypeGebruiker.Equals(TypeGebruiker.Lid) && g.Lesformule.Equals(formule)).ToList();
        }

        public Gebruiker GetBy(String gebruikersnaam)
        {
            return _gebruikers.Include(g => g.Adres).SingleOrDefault(g => g.Gebruikersnaam == gebruikersnaam); ;
        }

        public void Save(Gebruiker gebruiker)
        {
            _gebruikers.Add(gebruiker);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
