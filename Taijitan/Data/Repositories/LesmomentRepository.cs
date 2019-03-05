using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
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
            //_lesmomenten = _context.Lesmomenten;
        }

        public void Save(Lesmoment lesmoment)
        {
            _lesmomenten.Add(lesmoment);
            _context.SaveChanges();
        }
    }
}
