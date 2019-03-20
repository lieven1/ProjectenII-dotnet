using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taijitan.Filters;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;
using Taijitan.Models.Domain.IRepositories;

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

        public IActionResult KiesGraad(int graad) {
            HttpContext.Session.SetInt32("Graad", graad);
            return RedirectToAction("ThemaOverzicht");
        }
        
        public IActionResult ThemaOverzicht() {
            var graad = HttpContext.Session.GetInt32("Graad");
            var themas = _themaRepo.GetAll().FindAll(thema => thema.Lesmateriaal.Any(l => l.Graad.Equals((Gradatie)graad)));
            return View(themas);
        }

        public IActionResult LesmateriaalOverzicht(int themaId) {
            var thema = _themaRepo.GetBy(themaId);
            var graad = HttpContext.Session.GetInt32("Graad");
            var lesmateriaal = _themaRepo.GetLesmateriaal(thema, (Gradatie)graad);
            return View(lesmateriaal);
        }
    }
}