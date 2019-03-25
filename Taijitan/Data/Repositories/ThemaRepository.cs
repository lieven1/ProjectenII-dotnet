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
            return _themas.ToList();
        }

        public Thema GetBy(int id) {
            return _themas.FirstOrDefault(t => t.ThemaId == id);
        }

        

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
