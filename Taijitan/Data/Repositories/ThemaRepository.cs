using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;
using Taijitan.Models.Domain.IRepositories;

namespace Taijitan.Data.Repositories {
    public class ThemaRepository : IThemaRepository {
        private readonly DbSet<Thema> _themas;
        private readonly ApplicationDbContext _context;

        public ThemaRepository(ApplicationDbContext context) {
            this._context = context;
            this._themas = context.Themas;
        }

        public List<Thema> GetAll() {
            return _themas.Include(t => t.Lesmateriaal).ToList();
        }

        public Thema GetBy(string name) {
            return _themas.Include(t => t.Lesmateriaal).FirstOrDefault(t => t.Naam == name);
        }

        public List<Lesmateriaal> GetLesmateriaal(Thema thema, Gradatie graad) {
            return thema.Lesmateriaal.Where(l => l.Graad == graad).ToList();
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
