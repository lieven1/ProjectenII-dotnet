using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Taijitan.Controllers
{
    public class LesmateriaalController : Controller
    {
        private object @object;

        public LesmateriaalController(object @object) {
            this.@object = @object;
        }

        public IActionResult Index(string gebruikersnaam)
        {
            // TO DO - Implement
            return View();
        }
    }
}