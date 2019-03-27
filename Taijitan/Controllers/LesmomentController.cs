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
    [Authorize(Policy = "Beheerder")]
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
        
        public IActionResult BeheerLesmoment()
        {
            try
            {
                List<Lesmoment> lesmomenten = geefLesmomenten();
                return View(lesmomenten.OrderBy(l => l.StartTijd.Date));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult StartLesmoment(int id)
        {
            try
            {
                if (lesmomentRepository.GetAll().Exists(t => t.Actief))
                {
                    TempData["Error"] = "Er kan maar 1 lesmoment actief zijn.";
                    return RedirectToAction(nameof(BeheerLesmoment));
                }
                Lesmoment lesmoment = lesmomentRepository.GetById(id);
                lesmoment.ZetActief(true);
                lesmomentRepository.Save();
                return RedirectToAction(nameof(Aanwezigheden));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult StopLesmoment(int id)
        {
            try
            {
                Lesmoment lesmoment = lesmomentRepository.GetById(id);
                lesmoment.ZetActief(false);
                lesmomentRepository.Save();
                return RedirectToAction(nameof(BeheerLesmoment));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Aanwezigheden()
        {
            try
            {
                Lesmoment lesmoment = geefLesmomenten(l => l.Actief).FirstOrDefault();
                if (lesmoment == null)
                {
                    TempData["error"] = "Er is geen lesmoment bezig.";
                    return RedirectToAction("Index", "Home");
                }
                return View("Aanwezigheden", new LesmomentAlgemeenViewModel(lesmoment, LesformulesMetGebruikers()));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        
        public IActionResult GebruikersPerFormule(int lesmomentId, int lesformuleId)
        {
            try
            {
                Lesformule formule = lesformuleRepository.GetById(lesformuleId);
                Lesmoment lesmoment = lesmomentRepository.GetById(lesmomentId);
                List<Gebruiker> gebruikers = gebruikerRepository.GetAllLedenInFormule(formule);
                if(formule == null || lesmoment == null || gebruikers == null)
                {
                    return RedirectToAction("Error", "Home");
                }

                return View(new LesmomentGebruikersInFormuleViewModel(lesmoment, formule, gebruikers));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        
        public IActionResult RegistreerAanwezigheid(int lesmomentId, string gebruikersnaam)
        {
            try
            {
                Lesmoment lesmoment = lesmomentRepository.GetById(lesmomentId);
                Gebruiker gebruiker = gebruikerRepository.GetBy(gebruikersnaam);
                if (lesmoment.EersteHelftIsVoorbij())
                {
                    TempData["error"] = "De eerste helft van het lesmoment is al voorbij, u kan zelf niet meer aanwezig melden";
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                else
                {
                    if (gebruiker.Lesformule.TitleText.Count() > 8) { //meerdere dagen formule => 5 punten
                        gebruiker.voegPuntenToe(5);
                    } else {
                        gebruiker.voegPuntenToe(10);                 //1 dag formule => 10 punten
                    }
                    gebruikerRepository.SaveChanges();
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
            try
            {
                Lesmoment lesmoment = lesmomentRepository.GetById(id);
                if (lesmoment == null)
                {
                    return RedirectToAction(nameof(Aanwezigheden));
                }
                var ingeschrevenGebruikers = lesmoment.Leden.Select(l => l.Gebruikersnaam);
                var gebruikers = gebruikerRepository.GetAllLeden().OrderBy(g => g.Gebruikersnaam).Where(g => !ingeschrevenGebruikers.Contains(g.Gebruikersnaam));
                return View(new LesmomentNietIngeschrevenViewModel(gebruikers, lesmoment));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult RegistreerAanwezigheidProefles(int id)
        {
            try
            {
                return View(new LesmomentProeflesViewModel(lesmomentRepository.GetById(id)));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult RegistreerAanwezigheidProefles(LesmomentProeflesViewModel model)
        {
            try
            {
                Lesmoment lesmoment = lesmomentRepository.GetById(model.LesmomentId);

                Gebruiker gebruiker = LesmomentProeflesViewModelToGebruiker(model);
                gebruikerRepository.Save(gebruiker);

                lesmoment.RegistreerLid(gebruiker);
                lesmomentRepository.Save();
                return Aanwezigheden();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        private List<Lesmoment> geefLesmomenten(Func<Lesmoment, bool> predicate = null)
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
