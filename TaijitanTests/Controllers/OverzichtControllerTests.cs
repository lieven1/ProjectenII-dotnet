using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using Taijitan.Controllers;
using Taijitan.Models;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.IRepositories;
using Taijitan.Models.LesmomentViewModels;
using TaijitanTests.Data;
using Xunit;

namespace TaijitanTests.Controllers
{
   public class OverzichtControllerTests
    {
        private readonly OverzichtController _controller;
        private readonly DummyDBcontext _context;
        private readonly Mock<ILesmomentRepository> _lesmomentRepository;

        public OverzichtControllerTests()
        {
            var mockHttpContext = new Mock<HttpContext>();
            var tempDataProvider = new Mock<SessionStateTempDataProvider>();
            _context = new DummyDBcontext();
            _lesmomentRepository = new Mock<ILesmomentRepository>();


            _controller = new OverzichtController(_lesmomentRepository.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object
                },
                TempData = new TempDataDictionary(mockHttpContext.Object, tempDataProvider.Object)
            };
            _controller.TempData.Add("error", new TempDataAttribute());
        }

        #region Index()
        [Fact]
        public void Index_LoadGeenLesmomenten_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(_context.GeenLesmomenten);
            var actionResult = _controller.Index() as ViewResult;
            Assert.IsType<LesmomentOverzichtEditViewmodel>(actionResult?.Model);
        }

        [Fact]
        public void Index_LoadWelLesmomenten_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(_context.Lesmomenten);
            var actionResult = _controller.Index() as ViewResult;
            Assert.IsType<LesmomentOverzichtEditViewmodel>(actionResult?.Model);
        }
        #endregion

        #region Index(LesmomentOverzichtEditViewmodel model)
        [Fact]
        public void Index_NullViewModel_RedirectToError()
        {
            
        }

        [Fact]
        public void Index_LeegViewModel_RedirectToError()
        {

        }

        [Fact]
        public void Index_CorrectViewModel_Valid()
        {
            List<Lesmoment> lessen = new List<Lesmoment>();
            lessen.Add(new Lesmoment(new DateTime(2018,1,17), new DateTime(2018, 1, 17).AddHours(2)));
            LesmomentOverzichtEditViewmodel temp = new LesmomentOverzichtEditViewmodel(2018, Taijitan.Models.Domain.Enums.Maanden.Januari,lessen);

            var result = _controller.Index(temp) as ViewResult;
            var model = result.Model;

            Assert.IsType<LesmomentOverzichtEditViewmodel>(model);
        }
        #endregion
    }
}
