using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Databindings;
using Taijitan.Models.Domain.Enums;

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

        public void GenereerLesmomentDag()
        {
            DateTime now = DateTime.Now;
            DateTime start, end;
            switch ((int)now.DayOfWeek)
            {
                // Zondag
                case 0:
                    start = now.Date + new TimeSpan(11, 00, 00);
                    end = now.Date + new TimeSpan(12, 30, 00);
                    // Alle gebruikers (geen formule voorzien)
                    _lesmomenten.Add(new Lesmoment(start, end, _gebruikers.ToList()));
                    break;
                // Maandag
                case 1:
                    // Geen les
                    break;
                // Dinsdag
                case 2:
                    start = now.Date + new TimeSpan(18, 00, 00);
                    end = now.Date + new TimeSpan(19, 00, 00);
                    _lesmomenten.Add(new Lesmoment(start, end, _gebruikers.Where(
                        g => g.Lesformule.Equals(Lesformule.DinsdagZaterdag) ||
                        g.Lesformule.Equals(Lesformule.DinsdagDonderdag) ||
                        g.Lesformule.Equals(Lesformule.Dinsdag)).ToList()));
                    break;
                // Woensdag
                case 3:
                    start = now.Date + new TimeSpan(14, 00, 00);
                    end = now.Date + new TimeSpan(16, 00, 00);
                    _lesmomenten.Add(new Lesmoment(start, end, _gebruikers.Where(
                        g => g.Lesformule.Equals(Lesformule.Woensdag) ||
                        g.Lesformule.Equals(Lesformule.WoensdagZaterdag)).ToList()));
                    break;
                // Donderdag
                case 4:
                    start = now.Date + new TimeSpan(18, 00, 00);
                    end = now.Date + new TimeSpan(20, 00, 00);
                    _lesmomenten.Add(new Lesmoment(start, end, _gebruikers.Where(
                        g => g.Lesformule.Equals(Lesformule.DinsdagDonderdag)).ToList()));
                    break;
                // Vrijdag
                case 5:
                    // Geen les
                    break;
                // Zaterdag
                case 6:
                    start = now.Date + new TimeSpan(11, 30, 00);
                    end = now.Date + new TimeSpan(13, 00, 00);
                    _lesmomenten.Add(new Lesmoment(start, end, _gebruikers.Where(
                        g => g.Lesformule.Equals(Lesformule.DinsdagZaterdag) ||
                        g.Lesformule.Equals(Lesformule.WoensdagZaterdag) ||
                        g.Lesformule.Equals(Lesformule.Zaterdag)).ToList()));
                    break;
            }
            _context.SaveChanges();
        }
    }
}
