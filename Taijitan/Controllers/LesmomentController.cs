using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;
using Taijitan.Models.LesmomentViewModels;

namespace Taijitan.Controllers
{
    public class LesmomentController : Controller
    {
        private ILesmomentRepository lesmomentRepository;
        private IGebruikerRepository gebruikerRepository;

        public LesmomentController(ILesmomentRepository lesmomentRepository, IGebruikerRepository gebruikerRepository)
        {
            this.lesmomentRepository = lesmomentRepository;
            this.gebruikerRepository = gebruikerRepository;
        }

        public IActionResult Aanwezigheden()
        {
            Lesmoment lesmoment = geefLesmomenten(l => l.Gestart).FirstOrDefault();
            if (lesmoment == null) {
                TempData["error"] = "Er is geen lesmoment gestart. Probeer opnieuw wanneer er een lesmoment gestart is.";
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View("Aanwezigheden", new LesmomentGebruikerViewModel(lesmoment));
        }

        [Authorize(Policy = "Beheerder")]
        public IActionResult StartLesmoment() {
            List<Lesmoment> lesmomenten = geefLesmomenten();
            List<Lesmoment> lesmomentenGestart = geefLesmomenten(l => l.Gestart);
            if (lesmomentenGestart.Count > 0 && lesmomentenGestart[0].EindTijd.CompareTo(DateTime.Now) > 0) { //eindtijd is later dan nu => lesmoment is nog bezig
                TempData["error"] = "Er is al een lesmoment bezig.";
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(lesmomenten.OrderBy(l => l.Datum));
        }


        public IActionResult Start(int id)
        {
            Lesmoment lesmoment = lesmomentRepository.GetById(id);

            if (lesmoment != null)
            {
                lesmoment.startLesmoment();
                lesmomentRepository.Save();
                return Aanwezigheden();
            }
            else
            {
                // TODO
                // er ging iets mis => error boodschap duidelijker
                return NotFound();
            }

        }


        [Route("/Lesmoment/RegistreerAanwezigheid",
       Name = "registreeraanwezigheid")]
        public IActionResult RegistreerAanwezigheid(int lesmomentId, string gebruikersnaam)
        {
            Lesmoment lesmoment = lesmomentRepository.GetById(lesmomentId);
            Gebruiker gebruiker = gebruikerRepository.GetBy(gebruikersnaam);
            if (lesmoment == null || gebruiker == null)
            {
                //TODO error
                return NotFound();
            }
            else
            {
                lesmoment.RegistreerLid(gebruiker);
                lesmomentRepository.Save();
                return Aanwezigheden();
            }
        }

        public IActionResult RegistreerAanwezigheidNietIngeschreven(int id)
        {
            Lesmoment lesmoment = lesmomentRepository.GetById(id);
            var IngeshrevenGebruikers = lesmoment.Leden.Select(l => l.Gebruikersnaam);
            var gebruikers = gebruikerRepository.GetAllLeden().OrderBy(g => g.Gebruikersnaam).Where(g => !IngeshrevenGebruikers.Contains(g.Gebruikersnaam));
            return View(new LesmomentNietIngeschrevenViewModel(gebruikers, lesmoment));
        }

        public IActionResult RegistreerAanwezigheidProefles(int id) {
            return View(new LesmomentdProeflesViewModel(lesmomentRepository.GetById(id)));
        }

        public List<Lesmoment> geefLesmomenten(Func<Lesmoment,bool> predicate = null) {
            if (predicate == null) {
                return lesmomentRepository.GetAll();
            } else {
                return lesmomentRepository.GetAll().Where(predicate).ToList();
            }
        }

    }
}
