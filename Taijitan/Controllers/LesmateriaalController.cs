﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Taijitan.Filters;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;
using Taijitan.Models.Domain.IRepositories;
using Taijitan.Models.LesmateriaalViewModels;

namespace Taijitan.Controllers
{
    [Authorize]
    public class LesmateriaalController : Controller
    {
        private ILesmateriaalRepository _lesmateriaalRepo;
        private IThemaRepository _themaRepo;

        public LesmateriaalController(ILesmateriaalRepository lesmateriaalRepo, IThemaRepository themaRepo) {
            this._lesmateriaalRepo = lesmateriaalRepo;
            this._themaRepo = themaRepo;
        }

        [ServiceFilter(typeof(GebruikerFilter))]
        public IActionResult Overzicht(Gebruiker gebruiker, int gradatieInt = 0, int themaId = 0) {
            try
            {
                IEnumerable<Lesmateriaal> lesmateriaal;
                var gradatieGebruiker = gebruiker.Gradatie;
                if (gradatieInt == 0 && themaId == 0)
                {
                    lesmateriaal = _lesmateriaalRepo.GetAll().Where(l => l.Graad <= gradatieGebruiker);
                }
                else if (gradatieInt == 0 && themaId != 0)
                {
                    var thema = _themaRepo.GetBy(themaId);
                    lesmateriaal = _lesmateriaalRepo.GetAll().Where(l => l.Graad <= gradatieGebruiker && l.Thema == thema);
                }
                else if (themaId == 0 && gradatieInt != 0)
                {
                    lesmateriaal = _lesmateriaalRepo.GetAll().Where(l => l.Graad == (Gradatie)gradatieInt);
                }
                else
                {
                    var thema = _themaRepo.GetBy(themaId);
                    lesmateriaal = _lesmateriaalRepo.GetAll().Where(l => l.Graad == (Gradatie)gradatieInt && l.Thema == thema);
                }
                ViewData["Graden"] = MapGradenNaarSelectList(gradatieGebruiker, gradatieInt);
                ViewData["Themas"] = MapThemasNaarSelectList(themaId);
                return View(lesmateriaal);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        
        public IActionResult Lesmateriaal(int id)
        {
            try
            {
                var lesmateriaal = _lesmateriaalRepo.GetById(id);
                if (lesmateriaal == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(lesmateriaal);
                }
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        private SelectList MapThemasNaarSelectList(int selected) {
            var themas = _themaRepo.GetAll().OrderBy(t => t.Naam);
            return new SelectList(themas,
                "ThemaId", "Naam", selected);
        }

        private SelectList MapGradenNaarSelectList(Gradatie gradatieGebruiker, int selected) {
            var graden = Enum.GetValues(typeof(Gradatie)).Cast<Gradatie>()
                .Select(g => new GradatieViewModel((int)Convert.ChangeType(g,TypeCode.Int32))).ToList()
                .Where(g => g.gradatie.CompareTo(gradatieGebruiker) <= 0);
            return new SelectList(graden,
                "graadInt", "gradatie", selected);
        }
    }
}