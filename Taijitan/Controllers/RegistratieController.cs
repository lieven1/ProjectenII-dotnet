using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Taijitan.Models.Domain;
using Taijitan.Models.LesmomentViewModels;

namespace Taijitan.Controllers
{
    public class RegistratieController : Controller
    {
        private IGebruikerRepository _gebruikersRepository;
        private ILesmomentRepository _lesmomentRepository;
        
        public RegistratieController(IGebruikerRepository gebruikerRepository, ILesmomentRepository lesmomentRepository)
        {
            this._gebruikersRepository = gebruikerRepository;
            this._lesmomentRepository = lesmomentRepository;
        }

        //Beheerder maakt lesmoment
        //[Authorize(Roles = "Beheerder")]
        [HttpGet]
        public IActionResult MaakLesmoment()
        {
            LesmomentCreateViewModel createView = new LesmomentCreateViewModel();
            _gebruikersRepository.GetAllLeden().ForEach(g => createView.Gebruikers.Add(new GebruikerVM(g)));
            return View(createView);
        }

        //[Authorize(Roles = "Beheerder")]
        [HttpPost]
        public IActionResult MaakLesmoment(LesmomentCreateViewModel createView)
        {
            try
            {
                MapLesmomentCreateViewModelToLesmoment(createView);
                TempData["message"] = $"Je hebt een nieuw lesmoment aangemaakt.";
            }
            catch
            {
                TempData["error"] = "Er vond een probleem plaats bij aanmaken van het lesmoment.";
            }
            return RedirectToAction(nameof(RegistreerAanwezigheid));
        }

        private void MapLesmomentCreateViewModelToLesmoment(LesmomentCreateViewModel model)
        {
            Lesmoment lesmoment = new Lesmoment(model.Starttijd, model.Eindtijd, model.Gebruikers.Where(g => g.IsSelected.Equals(true)).Select(g => g.Gebruiker).ToList());
            _lesmomentRepository.Save(lesmoment);
        }

        public IActionResult RegistreerAanwezigheid()
        {
            return View();
        }
    }
}