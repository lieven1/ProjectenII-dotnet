using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taijitan.Models.Domain;

namespace Taijitan.Controllers
{
    public class RegistratieController : Controller
    {
        private List<Lid> leden;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegistreerAanwezigheid()
        {
            return View();
        }
    }
}