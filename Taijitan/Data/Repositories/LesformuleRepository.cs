using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;
using Taijitan.Models.Domain.IRepositories;

namespace Taijitan.Data.Repositories
{
    public class LesformuleRepository : ILesformuleRepository
    {
        private ApplicationDbContext _context;
        private DbSet<Lesformule> _lesformules;
        public LesformuleRepository(ApplicationDbContext context)
        {
            _context = context;
            _lesformules = context.Lesformules;
        }
        public IEnumerable<Lesformule> GetAll()
        {
            return _lesformules;
        }

        public Lesformule GetById(int id)
        {
            return _lesformules.FirstOrDefault(f => f.Id == id);
        }
    }
}
