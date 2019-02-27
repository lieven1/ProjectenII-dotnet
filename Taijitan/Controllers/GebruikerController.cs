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
        
        public IActionResult Index(Gebruiker gebruiker) {
            return RedirectToAction(nameof(Edit), gebruiker);
        }

        [ServiceFilter(typeof(GebruikerFilter))]
        [HttpGet]
        public IActionResult Edit(Gebruiker gebruiker) {
            if (gebruiker == null)
                return NotFound();
            return View(new GebruikerEditViewModel(gebruiker));
        }

        [HttpPost]
        public IActionResult Edit(Gebruiker gebruiker, GebruikerEditViewModel model) {
            try {
                MapGebruikerEditViewModelToGebruiker(model, gebruiker);
                _gebruikerRepository.SaveChanges();
                TempData["message"] = $"You successfully updated your data.";
            } catch {
                TempData["error"] = "Sorry, something went wrong, the data was not edited...";
            }
            return RedirectToAction(nameof(Edit));
        }
        private void MapGebruikerEditViewModelToGebruiker(GebruikerEditViewModel model, Gebruiker gebruiker) {
            gebruiker.Naam = model.Naam;
            gebruiker.Voornaam = model.Voornaam;
            gebruiker.Email = model.Email;
            gebruiker.Geboortedatum = model.Geboortedatum;
            gebruiker.Telefoonnummer = model.TelefoonNummer;
            gebruiker.Adres = new Adres(model.Land, model.Postcode, model.Stad, model.Straat, model.Nummer);
        }
    }
}