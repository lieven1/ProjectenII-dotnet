using Microsoft.EntityFrameworkCore;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.IRepositories;

namespace Taijitan.Data.Repositories {
    public class RaadplegingRepository : IRaadplegingRepository {
        private ApplicationDbContext _context;
        private DbSet<Raadpleging> _raadplegingen;

        public RaadplegingRepository(ApplicationDbContext context) {
            this._context = context;
            this._raadplegingen = context.Raadplegingen;
        }

        public void AddRaadpleging(Raadpleging raadpleging) {
            _raadplegingen.Add(raadpleging);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
