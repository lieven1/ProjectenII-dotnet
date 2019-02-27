using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taijitan.Models.Domain;
using Taijitan.Models.GebruikerViewModels;

namespace Taijitan.Controllers
{
    public class GebruikerController : Controller
    {
        private IGebruikerRepository gebruikerRepo;

        public GebruikerController(IGebruikerRepository gebruikerRepo) {
            this.gebruikerRepo = gebruikerRepo;
        }

        public IActionResult Index(Gebruiker lid)   //authenticatie implementeren
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int v)
        {
            var gebruiker = gebruikerRepo.GetById(1);               //tijdelijk aangezien er nog geen authenticatie is
            ViewData["Title"] = String.Format("Wijzigen persoonlijke gegevens");
            return View(new GebruikerEditViewModel(gebruiker));
        }

        [HttpPost]
        public IActionResult Edit(int v, GebruikerEditViewModel model)
        {
            try
            {
                Gebruiker gebruiker = gebruikerRepo.GetById(1);
                MapGebruikerEditViewModelToGebruiker(model, gebruiker);
                gebruikerRepo.SaveChanges();
                ViewData["Title"] = String.Format("Wijzigen persoonlijke gegevens");
                TempData["message"] = $"You successfully updated your data.";
            }
            catch
            {
                TempData["error"] = "Sorry, something went wrong, the data was not edited...";
            }
            return RedirectToAction(nameof(Index));
        }

        private void MapGebruikerEditViewModelToGebruiker(GebruikerEditViewModel model, Gebruiker gebruiker)
        {
            gebruiker.WijzigGegevens(model.Naam,model.Voornaam,model.Geboortedatum,model.TelefoonNummer,model.Email,model.Land,model.Postcode,model.Stad,model.Straat,model.Nummer);
        }
    }
   
}