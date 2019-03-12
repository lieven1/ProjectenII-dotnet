using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            List<Lesmoment> lesmomenten = lesmomentRepository.GetAll();

            return View(lesmomenten.OrderBy(l => l.Datum));
        }

        [HttpGet]
        public IActionResult Get(LesmomentGebruikerViewModel model)
        {
            if (model == null)
            {
                // iets ging mis TODO
                return NotFound();
            }
            else
            {
                return View("Get", model);
            }
        }


        public IActionResult Start(int id)
        {
            Lesmoment lesmoment = lesmomentRepository.GetById(id);
            LesmomentGebruikerViewModel model = new LesmomentGebruikerViewModel(lesmoment);

            if (lesmoment != null)
            {
                lesmoment.startLesmoment();
                return Get(model);
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
                LesmomentGebruikerViewModel model = new LesmomentGebruikerViewModel(lesmoment);
                return Get(model);
            }
        }

        public IActionResult RegistreerAanwezigheidNietIngeschreven(int id)
        {
            Lesmoment lesmoment = lesmomentRepository.GetById(id);
            var IngeshrevenGebruikers = lesmoment.Leden.Select(l => l.Gebruikersnaam);
            var gebruikers = gebruikerRepository.GetAllLeden().OrderBy(g => g.Gebruikersnaam).Where(g => !IngeshrevenGebruikers.Contains(g.Gebruikersnaam));
            return View(new LesmomentNietIngeschrevenViewModel(gebruikers, lesmoment));
        }
    }
}
