using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Taijitan.Models.Domain;

namespace Taijitan.Data.Repositories
{
    public class LesmomentRepository : ILesmomentRepository
    {
        private readonly DbSet<Lesmoment> _lesmomenten;
        private readonly ApplicationDbContext _context;

        public LesmomentRepository(ApplicationDbContext context)
        {
            _context = context;
            _lesmomenten = _context.Lesmomenten;
        }

        public void Save(Lesmoment lesmoment)
        {
            _lesmomenten.Add(lesmoment);
            _context.SaveChanges();
        }

        public List<Lesmoment> GetAll()
        {
            return _lesmomenten.OrderBy(l => l.Datum).ToList();
        }
    }
}
