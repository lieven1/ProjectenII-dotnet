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

        [Authorize(Policy = "Beheerder")]
        public IActionResult  BeheerLesmoment() {
            List<Lesmoment> lesmomenten = geefLesmomenten();
            return View(lesmomenten.OrderBy(l => l.Datum));
        }

        public IActionResult StartLesmoment(int id) {
            Lesmoment lesmoment = lesmomentRepository.GetById(id);
            lesmoment.ZetActief(true);
            lesmomentRepository.Save();
            return RedirectToAction(nameof(BeheerLesmoment));
        }

        public IActionResult StopLesmoment(int id) {
            Lesmoment lesmoment = lesmomentRepository.GetById(id);
            lesmoment.ZetActief(false);
            lesmomentRepository.Save();
            return RedirectToAction(nameof(BeheerLesmoment));
        }

        public IActionResult ToonActieveLesmomenten() {
            return View("ToonActieveLesmomenten", new LesmomentActiefViewModel(geefLesmomenten(l => l.Actief == true)));
        }

        public IActionResult Aanwezigheden(int id)
        {
            Lesmoment lesmoment = lesmomentRepository.GetById(id);
            return View("Aanwezigheden", new LesmomentGebruikerViewModel(lesmoment));
        }


        public IActionResult Start(int id)
        {
            Lesmoment lesmoment = lesmomentRepository.GetById(id);

            if (lesmoment != null)
            {
                return Aanwezigheden(lesmoment.LesmomentId);
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
                return Aanwezigheden(lesmoment.LesmomentId);
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
