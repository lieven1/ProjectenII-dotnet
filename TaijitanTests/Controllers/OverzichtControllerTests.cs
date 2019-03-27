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
            _lesmomentRepository.Setup(v => v.GetAfgelopenLesmomentenByYearAndMonth(DateTime.Now.Year, DateTime.Now.Month)).Returns(_context.GeenLesmomenten);
            _lesmomentRepository.Setup(v => v.GetJarenInDatabase()).Returns(new List<int>());
            var actionResult = _controller.Index() as ViewResult;
            Assert.IsType<LesmomentOverzichtEditViewmodel>(actionResult?.Model);
        }

        [Fact]
        public void Index_LoadWelLesmomenten_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetAfgelopenLesmomentenByYearAndMonth(DateTime.Now.Year, DateTime.Now.Month)).Returns(_context.Lesmomenten);
            _lesmomentRepository.Setup(v => v.GetJarenInDatabase()).Returns(new List<int>());
            var actionResult = _controller.Index() as ViewResult;
            Assert.IsType<LesmomentOverzichtEditViewmodel>(actionResult?.Model);
        }
        #endregion

        #region Index(LesmomentOverzichtEditViewmodel model)
        [Fact]
        public void Index_NullViewModel_RedirectToError()
        {
            var actionResult = _controller.Index(null) as RedirectToActionResult;
            Assert.Equal(nameof(HomeController.Error), actionResult.ActionName);
        }

        [Fact]
        public void Index_LeegViewModel_RedirectToError()
        {
            var actionResult = _controller.Index(new LesmomentOverzichtEditViewmodel()) as RedirectToActionResult;
            Assert.Equal(nameof(HomeController.Error), actionResult.ActionName);
        }

        [Fact]
        public void Index_ValidArgument_Valid()
        {
            List<Lesmoment> lessen = new List<Lesmoment>();
            lessen.Add(new Lesmoment(new DateTime(2018,1,17), new DateTime(2018, 1, 17).AddHours(2)));
            LesmomentOverzichtEditViewmodel temp = new LesmomentOverzichtEditViewmodel(2018, Taijitan.Models.Domain.Enums.Maanden.Januari,lessen);
            _lesmomentRepository.Setup(l => l.GetAfgelopenLesmomentenByYear(temp.Year)).Returns(lessen);
            _lesmomentRepository.Setup(l => l.GetAfgelopenLesmomentenByYearAndMonth(temp.Year, (int)temp.Month)).Returns(lessen);
            _lesmomentRepository.Setup(l => l.GetJarenInDatabase()).Returns(new List<int>());
            var result = _controller.Index(temp) as ViewResult;
            var model = result.Model;

            Assert.IsType<LesmomentOverzichtEditViewmodel>(model);
        }
        #endregion

        #region AanwezighedenLesmoment(int id)
        [Fact]
        public void AanwezighedenLesmoment_invalidArguments_RedirectoToIndex()
        {
            _lesmomentRepository.Setup(v => v.GetById(-1)).Returns((Lesmoment)null);
            var result = _controller.AanwezighedenLesmoment(-1) as RedirectToActionResult;
            Assert.Equal(nameof(_controller.Index), result.ActionName);
        }

        [Fact]
        public void AanwezighedenLesmoment_ValidArguments_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns(_context.LesmomentValid);
            var result = _controller.AanwezighedenLesmoment(_context.LesmomentValid.LesmomentId) as ViewResult;
            Assert.IsType<LesmomentOverzichtAanwezigenViewModel>(result?.Model);
        }

        #endregion
    }
}
