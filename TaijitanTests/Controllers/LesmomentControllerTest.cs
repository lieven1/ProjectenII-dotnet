using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using Taijitan.Controllers;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.IRepositories;
using Taijitan.Models.LesmomentViewModels;
using TaijitanTests.Data;
using Xunit;

namespace TaijitanTests.Controllers
{
    public class LesmomentControllerTest
    {
        private readonly LesmomentController _controller;
        private readonly DummyDBcontext _context;
        private readonly Mock<ILesmomentRepository> _lesmomentRepository;
        private readonly Mock<IGebruikerRepository> _gebruikerRepository;
        private readonly Mock<ILesformuleRepository> _lesformuleRepository;


        public LesmomentControllerTest()
        {
            var mockHttpContext = new Mock<HttpContext>();
            var tempDataProvider = new Mock<SessionStateTempDataProvider>();
            _context = new DummyDBcontext();
            _lesmomentRepository = new Mock<ILesmomentRepository>();
            _gebruikerRepository = new Mock<IGebruikerRepository>();
            _lesformuleRepository = new Mock<ILesformuleRepository>();

            _controller = new LesmomentController(_lesmomentRepository.Object, _gebruikerRepository.Object, _lesformuleRepository.Object) {
                ControllerContext = new ControllerContext {
                    HttpContext = mockHttpContext.Object
                },
                TempData = new TempDataDictionary(mockHttpContext.Object, tempDataProvider.Object)
            };
            _controller.TempData.Add("error", new TempDataAttribute());
        }

        #region BeheerLesmoment
        [Fact]
        public void BeheerLesmoment_LoadGeenLesmomenten_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(_context.GeenLesmomenten);
            var actionResult = _controller.BeheerLesmoment() as ViewResult;
            var model = actionResult?.Model;
            Assert.IsAssignableFrom<IEnumerable<Lesmoment>>(model);
            Assert.Empty((IEnumerable)model);
        }

        [Fact]
        public void BeheerLesmoment_LoadWelLesmomenten_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(_context.Lesmomenten);
            var actionResult = _controller.BeheerLesmoment() as ViewResult;
            var model = actionResult?.Model;
            Assert.IsAssignableFrom<IEnumerable<Lesmoment>>(model);
            Assert.NotEmpty((IEnumerable)model);
        }

        #endregion

        #region startEnStopLesmoment
        [Fact]
        public void StartLesmoment_NV_Valid_gaatNaarAanwezigheden()
        {
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns(_context.LesmomentValid);
            var result = _controller.StartLesmoment(_context.LesmomentValid.LesmomentId) as RedirectToActionResult;
            Assert.Equal(nameof(_controller.Aanwezigheden), result.ActionName);
        }

        [Fact]
        public void StoptLesmoment_NV_Valid_gaatNaarBeheerLesmoment()
        {
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns(_context.LesmomentValid);
            var result = _controller.StopLesmoment(_context.LesmomentValid.LesmomentId) as RedirectToActionResult;
            Assert.Equal(nameof(_controller.BeheerLesmoment), result.ActionName);
        }

        #endregion

        #region RegistreerAanwezigheid
        [Fact]
        public void RegistreerAanwezigheid_invalidArguments_Valid()
        {
            _gebruikerRepository.Setup(v => v.GetBy("-1")).Returns((Gebruiker)null);
            _lesmomentRepository.Setup(v => v.GetById(-1)).Returns((Lesmoment)null);
            var result = _controller.RegistreerAanwezigheid(-1, "-1") as RedirectToActionResult;
            Assert.Equal(nameof(_controller.Aanwezigheden), result.ActionName);
        }

        [Fact]
        public void RegistreerAanwezigheid_validArguments_Valid()
        {
            _gebruikerRepository.Setup(v => v.GetBy(_context.GebruikerInLesmomentLedenVanLesmomentValid.Gebruikersnaam)).Returns(_context.GebruikerInLesmomentLedenVanLesmomentValid);
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns(_context.LesmomentValid);
            var actionResult = _controller.RegistreerAanwezigheid(_context.LesmomentValid.LesmomentId, _context.GebruikerInLesmomentLedenVanLesmomentValid.Gebruikersnaam) as ViewResult;
            Assert.IsType<LesmomentGebruikerViewModel>(actionResult?.Model);
        }

        #endregion

        #region Aanwezigheden
        [Fact]
        public void Aanwezigheden_invalidArguments_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetById(-1)).Returns((Lesmoment)null);
            var actionResult = _controller.Aanwezigheden() as ViewResult;
            Assert.IsType<LesmomentGebruikerViewModel>(actionResult?.Model);
        }

        [Fact]
        public void Aanwezigheden_validArguments_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns(_context.LesmomentValid);
            var actionResult = _controller.Aanwezigheden() as ViewResult;
            Assert.IsType<LesmomentGebruikerViewModel>(actionResult?.Model);
        }

        #endregion

        #region Aanwezigen
        /*
        [Fact]
        public void Aanwezigen_Valid() {
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(_context.Lesmomenten);
            var actionResult = _controller.Aanwezigen() as ViewResult;
            Assert.IsType<LesmomentGebruikerViewModel>(actionResult?.Model);
        }

        [Fact]
        public void Aanwezigen_geenLesmoment_gaatNaarIndex() {
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(_context.GeenLesmomenten);
            var actionResult = _controller.Aanwezigen() as RedirectToActionResult;
            Assert.Equal("Index", actionResult?.ActionName);
            Assert.True(_controller.TempData.ContainsKey("error"));
        }
        */
        #endregion
        
        #region RegistreerAanwezigheidNietIngeschreven
        [Fact]
        public void RegistreerAanwezigheidNietIngeschreven_invalidArguments_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetById(-1)).Returns((Lesmoment)null);
            var result = _controller.RegistreerAanwezigheid(-1, "-1") as RedirectToActionResult;
            Assert.Equal(nameof(_controller.Aanwezigheden), result.ActionName);
        }
        #endregion
    }
}
