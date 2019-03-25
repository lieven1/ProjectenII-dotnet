using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taijitan.Filters;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;
using Taijitan.Models.Domain.IRepositories;
using Taijitan.Models.LesmateriaalViewModels;

namespace Taijitan.Controllers {
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
        public IActionResult GraadOverzicht(Gebruiker gebruiker)
        {
            var gradatie = gebruiker.Gradatie;
            var graden = Enum.GetValues(typeof(Gradatie)).Cast<Gradatie>().ToList().Where(g => g.CompareTo(gradatie) <= 0)
                .OrderByDescending(graad => Convert.ChangeType(graad,TypeCode.Int32));
            if (graden.Count() == 1) {
                return RedirectToAction("ThemaOverzicht", gradatie);
            }
            return View(graden);
        }
        
        public IActionResult ThemaOverzicht(int graad) {
            var lesmateriaal = _lesmateriaalRepo.GetAll().Where(l => l.Graad == (Gradatie)graad);
            var themas = lesmateriaal.Select<Lesmateriaal, Thema>(l => l.Thema).Distinct().ToList();
            var themaViewModel = new ThemaViewModel(graad, themas);
            return View(themaViewModel);
        }

        public IActionResult LesmateriaalOverzicht(int themaId, int graad) {
            var thema = _themaRepo.GetBy(themaId);
            var lesmateriaal = _lesmateriaalRepo.GetAll().Where(l => l.Thema.ThemaId == themaId && 
                                                            (int)Convert.ChangeType(l.Graad, TypeCode.Int32) == graad);
            return View(lesmateriaal);
        }

        public IActionResult Lesmateriaal(int id) {
            var lesmateriaal = _lesmateriaalRepo.GetById(id);
            if(lesmateriaal == null) {
                return NotFound();
            } else {
                return View(lesmateriaal);
            }
        }
    }
}