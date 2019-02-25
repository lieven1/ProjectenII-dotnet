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
        private IGebruikerRepository lidRepo;

        public GebruikerController(IGebruikerRepository lidRepo) {
            this.lidRepo = lidRepo;
        }

        public IActionResult Index(Gebruiker lid)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public IActionResult Edit(int v) {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IActionResult Edit(int v, EditViewModel lidVM) {
            throw new NotImplementedException();
        }
    }
}