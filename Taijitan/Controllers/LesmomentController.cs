using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Taijitan.Models.Domain;

namespace Taijitan.Controllers
{
    public class LesmomentController : Controller
    {
        private ILesmomentRepository lesmomentRepository;

        public LesmomentController(ILesmomentRepository lesmomentRepository)
        {
            this.lesmomentRepository = lesmomentRepository;
        }

        public IActionResult Index()
        {
            List<Lesmoment> lesmomenten = lesmomentRepository.GetAll();

            return View(lesmomenten.OrderBy(l => l.Datum));
        }

        public IActionResult Get()
        {

        }
    }
}
