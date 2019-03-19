using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taijitan.Filters;
using Taijitan.Models;
using Taijitan.Models.Domain;

namespace Taijitan.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [ServiceFilter(typeof(AspUserToGebruiker))]
        public IActionResult Index(/*Gebruiker gebruiker*/) {
            return View();
            //if (User.IsInRole("gebruiker"))
            //{
            //    HttpContext.Session.SetString("Gebruiker", gebruiker.Gebruikersnaam);
            //    return RedirectToAction(nameof(Gebruiker));
            //}
            //else if(User.IsInRole("beheerder"))
            //{
            //    return View();
            //}
            //return RedirectToAction(nameof(Error));
        }

        public IActionResult Gebruiker(string gebruikersnaam)
        {
            //TempData["beheerder"] = User.IsInRole("beheerder");
            HttpContext.Session.SetString("Gebruiker", gebruikersnaam);
            return View();
        }

        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Aanwezigen", "Lesmoment");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
