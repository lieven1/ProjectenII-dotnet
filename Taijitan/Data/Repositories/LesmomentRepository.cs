using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Databindings;

namespace Taijitan.Data.Repositories
{
    public class LesmomentRepository : ILesmomentRepository
    {
        private readonly DbSet<Lesmoment> _lesmomenten;
        private readonly DbSet<LesmomentLeden> _lesmomentLeden;
        private readonly ApplicationDbContext _context;

        public LesmomentRepository(ApplicationDbContext context)
        {
            _context = context;
            _lesmomenten = _context.Lesmomenten;
            _lesmomentLeden = _context.LesmomentLeden;
        }

        public void Save(Lesmoment lesmoment)
        {
            _lesmomenten.Add(lesmoment);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public List<Lesmoment> GetAll()
        {
            List<Lesmoment> lesmomenten = _lesmomenten.OrderBy(l => l.Datum).ToList();
            foreach (var l in lesmomenten)
            {
                l.Leden = _lesmomentLeden.Include(lid => lid.Gebruiker).Where(m => m.LesmomentId == l.LesmomentId).ToList();
            }

            return lesmomenten;
        }

        public Lesmoment GetById(int id)
        {
            return _lesmomenten.Include(lesm => lesm.Leden).ThenInclude(lid => lid.Gebruiker).SingleOrDefault(l => l.LesmomentId == id);
        }
    }
}
