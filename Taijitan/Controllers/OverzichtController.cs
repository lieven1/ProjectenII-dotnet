﻿using System;
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
            LesmomentOverzichtEditViewmodel model = new LesmomentOverzichtEditViewmodel(DateTime.Now.Year,(Maanden)DateTime.Now.Month, lesmomentRepository.GetAfgelopenLesmomentenByYearAndMonth(DateTime.Now.Year, DateTime.Now.Month));
            ViewData["Jaren"] = new SelectList(lesmomentRepository.GetJarenInDatabase());
            ViewData["Maanden"] = new SelectList(new List<Maanden>() { Maanden.Januari , Maanden.Februari, Maanden.Maart, Maanden.April, Maanden.Mei, Maanden.Juni, Maanden.Juli, Maanden.Augustus, Maanden.September, Maanden.Oktober, Maanden.November, Maanden.December });
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(LesmomentOverzichtEditViewmodel model)
        {
            model.Lesmomenten = lesmomentRepository.GetAfgelopenLesmomentenByYearAndMonth(model.Year,(int) model.Month);
            ViewData["Jaren"] = new SelectList(lesmomentRepository.GetJarenInDatabase());
            ViewData["Maanden"] = new SelectList(new List<Maanden>() { Maanden.Januari, Maanden.Februari, Maanden.Maart, Maanden.April, Maanden.Mei, Maanden.Juni, Maanden.Juli, Maanden.Augustus, Maanden.September, Maanden.Oktober, Maanden.November, Maanden.December });
            return View(model);
        }

        public IActionResult AanwezighedenLesmoment(int id)
        {
            LesmomentOverzichtAanwezigenViewModel model = new LesmomentOverzichtAanwezigenViewModel(lesmomentRepository.GetById(id), lesmomentRepository.GetAanwezigenLesmomenten(id));
            return View(model);
        }
    }
}