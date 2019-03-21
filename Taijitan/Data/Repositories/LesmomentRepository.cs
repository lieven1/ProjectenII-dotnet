﻿using Microsoft.EntityFrameworkCore;
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
    }
}
