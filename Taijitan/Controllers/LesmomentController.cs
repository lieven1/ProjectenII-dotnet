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
    [Authorize(Policy = "Beheerder")]
    public class LesmomentController : Controller
    {
        private ILesmomentRepository lesmomentRepository;
        private IGebruikerRepository gebruikerRepository;

        public LesmomentController(ILesmomentRepository lesmomentRepository, IGebruikerRepository gebruikerRepository)
        {
            this.lesmomentRepository = lesmomentRepository;
            this.gebruikerRepository = gebruikerRepository;
        }
        
        public IActionResult BeheerLesmoment()
        {
            List<Lesmoment> lesmomenten = geefLesmomenten();
            return View(lesmomenten.OrderBy(l => l.StartTijd.Date));
        }

        public IActionResult StartLesmoment(int id)
        {
            if(lesmomentRepository.GetAll().Exists(t => t.Actief))
            {
                TempData["Error"] = "Er kan maar 1 lesmoment actief zijn.";
                return RedirectToAction(nameof(BeheerLesmoment));
            }
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

        public IActionResult Aanwezigheden()
        {
            Lesmoment lesmoment = geefLesmomenten(l => l.Actief).FirstOrDefault();
            if (lesmoment == null)
            {
                TempData["error"] = "Er is geen lesmoment bezig.";
                return RedirectToAction("Index", "Home");
            }
            return View("Aanwezigheden", new LesmomentAlgemeenViewModel(lesmoment, LesformulesMetGebruikers()));
        }
        
        public IActionResult GebruikersPerFormule(int lesmomentId, string lesformule)
        {
            Lesformule formule = (Lesformule)Enum.Parse(typeof(Lesformule), lesformule);
            Lesmoment lesmoment = lesmomentRepository.GetById(lesmomentId);

            List<Gebruiker> gebruikers = gebruikerRepository.GetAllLedenInFormule(formule);

            return View(new LesmomentGebruikersInFormuleViewModel(lesmoment, formule, gebruikers));
        }
        
        public IActionResult RegistreerAanwezigheid(int lesmomentId, string gebruikersnaam)
        {
            try
            {
                Lesmoment lesmoment = lesmomentRepository.GetById(lesmomentId);
                Gebruiker gebruiker = gebruikerRepository.GetBy(gebruikersnaam);
                if (lesmoment.Equals(null) || gebruiker.Equals(null))
                {
                    return RedirectToAction("Error", "Home");
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
                    return RedirectToAction(nameof(Aanwezigheden));
                }
            }
            catch
            {
                return RedirectToAction("Error", "Home");
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
            return View(new LesmomentProeflesViewModel(lesmomentRepository.GetById(id)));
        }

        [HttpPost]
        public IActionResult RegistreerAanwezigheidProefles(LesmomentProeflesViewModel model)
        {
            Lesmoment lesmoment = lesmomentRepository.GetById(model.LesmomentId);

            Gebruiker gebruiker = LesmomentProeflesViewModelToGebruiker(model);
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

        private Gebruiker LesmomentProeflesViewModelToGebruiker(LesmomentProeflesViewModel model)
        {
            return new Gebruiker(DateTime.Now, TypeGebruiker.Proefgebruiker, "proefles-" + DateTime.Now.TimeOfDay + "-" + model.Naam + "-" + model.Voornaam, model.Naam, model.Voornaam, model.Email, model.Telefoonnummer);
        }

        private HashSet<Lesformule> LesformulesMetGebruikers()
        {
            HashSet<Lesformule> lesformules = new HashSet<Lesformule>();
            gebruikerRepository.GetAllLeden().ForEach(g => lesformules.Add(g.Lesformule));
            return lesformules;
        }
    }
}
