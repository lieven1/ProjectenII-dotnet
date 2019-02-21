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
        private ILidRepository @object;

        public GebruikerController(ILidRepository @object) {
            this.@object = @object;
        }

        public IActionResult Index(Lid lid)
        {
            return View();
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