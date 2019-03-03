using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taijitan.Filters;
using Taijitan.Models.Domain;
using Taijitan.Models.GebruikerViewModels;

namespace Taijitan.Controllers {
    [Authorize]
    public class GebruikerController : Controller {
        private IGebruikerRepository _gebruikerRepository;

        public GebruikerController(IGebruikerRepository gebruikerRepository) {
            this._gebruikerRepository = gebruikerRepository;
        }
        
        public IActionResult Index() {
            return RedirectToAction(nameof(Edit));
        }
        
        [HttpGet]
        [ServiceFilter(typeof(GebruikerFilter))]
        public IActionResult Edit(Gebruiker gebruiker) {
            if (gebruiker == null)
                return NotFound();
            return View(new GebruikerEditViewModel(gebruiker));
        }

        [HttpPost]
        [ServiceFilter(typeof(GebruikerFilter))]
        public IActionResult Edit(Gebruiker gebruiker, GebruikerEditViewModel model) {
            try {
                MapGebruikerEditViewModelToGebruiker(model, gebruiker);
                _gebruikerRepository.SaveChanges();
                TempData["message"] = $"You successfully updated your data.";
            } catch {
                TempData["error"] = "Sorry, something went wrong, the data was not edited...";
                return RedirectToAction(nameof(Edit), model);
            }
            return RedirectToAction(nameof(Edit));
        }
        private void MapGebruikerEditViewModelToGebruiker(GebruikerEditViewModel model, Gebruiker gebruiker) {
            gebruiker.WijzigGegevens(model.Naam, model.Voornaam, model.TelefoonNummer, model.Gsmnummer, model.Email, model.EmailOuders, model.Land, model.Postcode, model.Stad, model.Straat, model.Nummer);
        }
    }
}