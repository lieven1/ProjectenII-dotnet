using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private readonly DbSet<Gebruiker> _gebruikers;
        private readonly ApplicationDbContext _context;

        public GebruikerRepository(ApplicationDbContext context) {
            _context = context;
            _gebruikers = _context.Gebruikers;
        }
        public Gebruiker GetBy(string email)
        {
            return _gebruikers.Include(g => g.Adres).SingleOrDefault(g => g.Email == email);
        }
        public Gebruiker GetById(int id)
        {
            return _gebruikers.Include(g => g.Adres).SingleOrDefault(g => g.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
