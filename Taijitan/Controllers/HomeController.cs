using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taijitan.Filters;
using Taijitan.Models;
using Taijitan.Models.Domain;

namespace Taijitan.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [ServiceFilter(typeof(AspUserToGebruiker))]
        public IActionResult Index(Gebruiker gebruiker) {
            try
            {

                if (User.IsInRole("gebruiker"))
                {
                    HttpContext.Session.SetString("Gebruiker", gebruiker.Gebruikersnaam);
                    return RedirectToAction(nameof(Dashboard));
                }
                else if (User.IsInRole("beheerder"))
                {
                    return View();
                }
                return RedirectToAction(nameof(Error));
            }
            catch
            {
                return RedirectToAction(nameof(Error));
            }
        }

        public IActionResult Login(String gebruikersnaam)
        {
            try
            {
                HttpContext.Session.SetString("Gebruiker", gebruikersnaam);
                return RedirectToAction(nameof(Dashboard));
            }
            catch
            {
                return RedirectToAction(nameof(Error));
            }
        }
        
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Aanwezigheden", "Lesmoment");
            }
            catch
            {
                return RedirectToAction(nameof(Error));
            }
        }

        public IActionResult Dashboard()
        {
            try
            {
                TempData["leeromgeving"] = User.IsInRole("beheerder");
                TempData["gebruiker"] = HttpContext.Session.GetString("Gebruiker");
                return View();
            }
            catch
            {
                return RedirectToAction(nameof(Error));
            }
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
