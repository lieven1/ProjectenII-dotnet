using Microsoft.EntityFrameworkCore;
using System;
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
        private readonly DbSet<Gebruiker> _gebruikers;
        private readonly ApplicationDbContext _context;

        public LesmomentRepository(ApplicationDbContext context)
        {
            _context = context;
            _lesmomenten = _context.Lesmomenten;
            _lesmomentLeden = _context.LesmomentLeden;
            _gebruikers = _context.Gebruikers;
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
            List<Lesmoment> lesmomenten = _lesmomenten.OrderBy(l => l.StartTijd.Date).ToList();
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

        public IEnumerable<Lesmoment> GetAfgelopenLesmomentenByYear(int year)
        {
            return _lesmomenten.Where(l => l.StartTijd.Year == year && l.EindTijd < DateTime.Now).OrderBy(l => l.StartTijd.Month).ThenBy(l => l.StartTijd.Day);
        }

        public IEnumerable<int> GetJarenInDatabase()
        {
            IEnumerable<int> jaren = new List<int>() { DateTime.Now.Year };
            _lesmomenten.ToList().ForEach(l =>
            {
                if (!jaren.Contains(l.StartTijd.Year)) { jaren.Append(l.StartTijd.Year); }
            });
            return jaren;
        }

        public IEnumerable<Lesmoment> GetAfgelopenLesmomentenByYearAndMonth(int year, int month)
        {
            return _lesmomenten.Where(l => (l.StartTijd.Year == year) && (l.EindTijd < DateTime.Now) && (l.StartTijd.Month == month)).OrderBy(l => l.StartTijd.Month).ThenBy(l => l.StartTijd.Day);

        }

        public IEnumerable<Gebruiker> GetAanwezigenLesmomenten(int id)
        {
                return _lesmomentLeden.Where(l => l.Lesmoment.LesmomentId == id && l.Aanwezig == true).Select(l => l.Gebruiker);             
        }

        public IEnumerable<Lesmoment> GetVolgendeWeek()
        {
            return _lesmomenten.Where(t => (t.EindTijd > DateTime.Now) && (t.StartTijd < DateTime.Now.AddDays(7)));
        }
    }
}
