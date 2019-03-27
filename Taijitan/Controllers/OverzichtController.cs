using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;
using Taijitan.Models.LesmomentViewModels;

namespace Taijitan.Controllers
{
    public class OverzichtController : Controller
    {
        private ILesmomentRepository lesmomentRepository;

        public OverzichtController(ILesmomentRepository lesmomentRepository)
        {
            this.lesmomentRepository = lesmomentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                LesmomentOverzichtEditViewmodel model = new LesmomentOverzichtEditViewmodel(DateTime.Now.Year, (Maanden)DateTime.Now.Month, lesmomentRepository.GetAfgelopenLesmomentenByYearAndMonth(DateTime.Now.Year, DateTime.Now.Month));
                ViewData["Jaren"] = new SelectList(lesmomentRepository.GetJarenInDatabase());
                ViewData["Maanden"] = new SelectList(new List<Maanden>() { Maanden.Alle, Maanden.Januari, Maanden.Februari, Maanden.Maart, Maanden.April, Maanden.Mei, Maanden.Juni, Maanden.Juli, Maanden.Augustus, Maanden.September, Maanden.Oktober, Maanden.November, Maanden.December });
                return View(model);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult Index(LesmomentOverzichtEditViewmodel model)
        {
            try
            {
                if (model.Year == 0 || (int)model.Month == 0)
                {
                    return RedirectToAction("Error", "Home");
                }
                else
                {
                    if (model.Month == Maanden.Alle)
                    {
                        model.Lesmomenten = lesmomentRepository.GetAfgelopenLesmomentenByYear(model.Year);
                    }
                    else
                    {
                        model.Lesmomenten = lesmomentRepository.GetAfgelopenLesmomentenByYearAndMonth(model.Year, (int)model.Month);
                    }
                    ViewData["Jaren"] = new SelectList(lesmomentRepository.GetJarenInDatabase());
                    ViewData["Maanden"] = new SelectList(new List<Maanden>() { Maanden.Alle, Maanden.Januari, Maanden.Februari, Maanden.Maart, Maanden.April, Maanden.Mei, Maanden.Juni, Maanden.Juli, Maanden.Augustus, Maanden.September, Maanden.Oktober, Maanden.November, Maanden.December });
                    return View(model);
                }
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }


        }

        public IActionResult AanwezighedenLesmoment(int id)
        {
            try
            {
                Lesmoment les = lesmomentRepository.GetById(id);
                if (les == null)
                {
                    return RedirectToAction("Index", "Overzicht");
                }
                else
                {
                    LesmomentOverzichtAanwezigenViewModel model = new LesmomentOverzichtAanwezigenViewModel(les, lesmomentRepository.GetAanwezigenLesmomenten(id), lesmomentRepository.GetAfwezeigenLesmomenten(id));
                    return View(model);
                }
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}