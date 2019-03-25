using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.IRepositories;

namespace Taijitan.Data.Repositories {
    public class LesmateriaalRepository : ILesmateriaalRepository {
        private readonly DbSet<Lesmateriaal> _lesmateriaal;
        private readonly ApplicationDbContext _context;

        public LesmateriaalRepository(ApplicationDbContext context) {
            _context = context;
            _lesmateriaal = _context.Lesmateriaal;
        }

        public List<Lesmateriaal> GetAll() {
            return _lesmateriaal.Include(l => l.Fotos).Include(l => l.Thema).ToList();
        }

        public Lesmateriaal GetById(int id) {
            return _lesmateriaal.Include(l => l.Fotos).ThenInclude(f => f.Foto).Include(l => l.Thema)
                .FirstOrDefault(l => l.LesmateriaalId == id);
        }
    }
}
