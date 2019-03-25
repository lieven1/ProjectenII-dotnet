using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;
using Taijitan.Models.Domain.IRepositories;
using Taijitan.Models.LesmomentViewModels;

namespace Taijitan.Controllers
{
    [Authorize]
    public class LesmomentController : Controller
    {
        private ILesmomentRepository lesmomentRepository;
        private IGebruikerRepository gebruikerRepository;
        private ILesformuleRepository lesformuleRepository;

        public LesmomentController(ILesmomentRepository lesmomentRepository, IGebruikerRepository gebruikerRepository, ILesformuleRepository lesformuleRepository)
        {
            this.lesmomentRepository = lesmomentRepository;
            this.gebruikerRepository = gebruikerRepository;
            this.lesformuleRepository = lesformuleRepository;
        }

        [Authorize(Policy = "Beheerder")]
        public IActionResult BeheerLesmoment()
        {
            List<Lesmoment> lesmomenten = geefLesmomenten();
            return View(lesmomenten.OrderBy(l => l.StartTijd.Date));
        }

        public IActionResult StartLesmoment(int id)
        {
            Lesmoment lesmoment = lesmomentRepository.GetById(id);
            lesmoment.ZetActief(true);
            lesmomentRepository.Save();
            return RedirectToAction(nameof(Aanwezigheden));
        }

        public IActionResult StopLesmoment(int id)
        {
            Lesmoment lesmoment = lesmomentRepository.GetById(id);
            lesmoment.ZetActief(false);
            lesmomentRepository.Save();
            return RedirectToAction(nameof(BeheerLesmoment));
        }

        //[Authorize(Policy = "Beheerder")]
        //public IActionResult ToonActieveLesmomenten()
        //{
        //    return View("ToonActieveLesmomenten", new LesmomentActiefViewModel(geefLesmomenten(l => l.Actief)));
        //}

        public IActionResult Aanwezigheden()
        {
            Lesmoment lesmoment = geefLesmomenten(l => l.Actief).FirstOrDefault();
            if (lesmoment == null)
            {
                TempData["error"] = "Er is geen lesmoment bezig.";
                return RedirectToAction("Index", "Home");
            }
            return View("Aanwezigheden", new LesmomentAlgemeenViewModel(lesmoment, lesformulesMetGebruikers()));
        }

        [Route("/Lesmoment/GebruikersPerFormule", Name = "gebruikersperformule")]
        public IActionResult GebruikersPerFormule(int lesmomentId, int lesformuleId)
        {
            Lesformule formule = lesformuleRepository.GetById(lesformuleId);
            Lesmoment lesmoment = lesmomentRepository.GetById(lesmomentId);

            List<Gebruiker> gebruikers = gebruikerRepository.GetAllLedenInFormule(formule);

            return View(new LesmomentGebruikersInFormuleViewModel(lesmoment, formule, gebruikers));
        }

        public IActionResult Aanwezigen()
        {
            Lesmoment lesmoment = geefLesmomenten(l => l.Actief).FirstOrDefault();
            if (lesmoment == null)
            {
                TempData["error"] = "Er is geen lesmoment bezig.";
                return RedirectToAction("Index", "Home");
            }
            return View("Aanwezigen", new LesmomentGebruikerViewModel(lesmoment));
        }

        //public IActionResult Start(int id)
        //{
        //    Lesmoment lesmoment = lesmomentRepository.GetById(id);

        //    if (lesmoment != null)
        //    {
        //        return Aanwezigheden(lesmoment.LesmomentId);
        //    }
        //    else
        //    {
        //        // TODO
        //        // er ging iets mis => error boodschap duidelijker
        //        return NotFound();
        //    }

        //}


        [Route("/Lesmoment/RegistreerAanwezigheid",
       Name = "registreeraanwezigheid")]
        public IActionResult RegistreerAanwezigheid(int lesmomentId, string gebruikersnaam)
        {
            Lesmoment lesmoment = lesmomentRepository.GetById(lesmomentId);
            Gebruiker gebruiker = gebruikerRepository.GetBy(gebruikersnaam);
            if (lesmoment == null || gebruiker == null)
            {
                return RedirectToAction(nameof(Aanwezigheden));
            }
            else if (lesmoment.EersteHelftIsVoorbij())
            {
                TempData["error"] = "De eerste helft van het lesmoment is al voorbij, u kan zelf niet meer aanwezig melden";
                return RedirectToRoute(new { controller = "Home", action = "Index" });
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
            if (lesmoment == null)
            {
                return RedirectToAction(nameof(Aanwezigheden));
            }
            var IngeshrevenGebruikers = lesmoment.Leden.Select(l => l.Gebruikersnaam);
            var gebruikers = gebruikerRepository.GetAllLeden().OrderBy(g => g.Gebruikersnaam).Where(g => !IngeshrevenGebruikers.Contains(g.Gebruikersnaam));
            return View(new LesmomentNietIngeschrevenViewModel(gebruikers, lesmoment));
        }

        [HttpGet]
        public IActionResult RegistreerAanwezigheidProefles(int id)
        {
            return View(new LesmomentdProeflesViewModel(lesmomentRepository.GetById(id)));
        }

        [HttpPost]
        public IActionResult RegistreerAanwezigheidProefles(LesmomentdProeflesViewModel model)
        {
            Lesmoment lesmoment = lesmomentRepository.GetById(model.LesmomentId);

            Gebruiker gebruiker = lesmomentdProeflesViewModelToGebruiker(model);
            gebruikerRepository.Save(gebruiker);

            lesmoment.RegistreerLid(gebruiker);
            lesmomentRepository.Save();
            return Aanwezigheden();
        }


        public List<Lesmoment> geefLesmomenten(Func<Lesmoment, bool> predicate = null)
        {
            if (predicate == null)
            {
                return lesmomentRepository.GetAll();
            }
            else
            {
                return lesmomentRepository.GetAll().Where(predicate).ToList();
            }
        }

        private Gebruiker lesmomentdProeflesViewModelToGebruiker(LesmomentdProeflesViewModel model)
        {
            return new Gebruiker(DateTime.Now, TypeGebruiker.Proefgebruiker, "proefles-" + DateTime.Now.TimeOfDay + "-" + model.Naam + "-" + model.Voornaam, model.Naam, model.Voornaam, model.Email, model.Telefoonnummer);
        }

        private HashSet<Lesformule> lesformulesMetGebruikers()
        {
            HashSet<Lesformule> lesformules = new HashSet<Lesformule>();
            gebruikerRepository.GetAllLeden().ForEach(g => lesformules.Add(g.Lesformule));
            return lesformules;
        }
    }
}
