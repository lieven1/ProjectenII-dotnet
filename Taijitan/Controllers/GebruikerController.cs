﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taijitan.Filters;
using Taijitan.Models.Domain;
using Taijitan.Models.GebruikerViewModels;

namespace Taijitan.Controllers
{
    [Authorize]
    public class GebruikerController : Controller {
        private IGebruikerRepository _gebruikerRepository;

        public GebruikerController(IGebruikerRepository gebruikerRepository) {
            this._gebruikerRepository = gebruikerRepository;
        }

        [ServiceFilter(typeof(GebruikerFilter))]
        public IActionResult Index(Gebruiker gebruiker) {
            try
            {
                if (gebruiker.Equals(null))
                {
                    return RedirectToAction("Error", "Home");
                }
                return View(gebruiker);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(GebruikerFilter))]
        public IActionResult Edit(Gebruiker gebruiker)
        {
            try
            {
                if (gebruiker.Equals(null))
                {
                    return RedirectToAction("Error", "Home");
                }
                return View(new GebruikerEditViewModel(gebruiker));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(GebruikerFilter))]
        public IActionResult Edit(Gebruiker gebruiker, GebruikerEditViewModel model)
        {
            try
            {

                if (gebruiker.Equals(null))
                {
                    return RedirectToAction("Error", "Home");
                }

                try
                {
                    MapGebruikerEditViewModelToGebruiker(model, gebruiker);
                    _gebruikerRepository.SaveChanges();
                    TempData["message"] = $"Je hebt je gegevens succesvol bijgewerkt.";
                }
                catch
                {
                    TempData["error"] = "Er vond een probleem plaats bij het wijzigen van je gegevens. Probeer later opnieuw.";
                    return RedirectToAction(nameof(Edit), model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        private void MapGebruikerEditViewModelToGebruiker(GebruikerEditViewModel model, Gebruiker gebruiker) {
            gebruiker.WijzigGegevens(model.Naam, model.Voornaam, model.TelefoonNummer, model.Gsmnummer, model.Email, model.EmailOuders, model.Land, model.Postcode, model.Stad, model.Straat, model.Nummer);
        }
        
    }
}