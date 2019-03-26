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
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(new List<Lesmoment>() { _context.LesmomentValid});
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns(_context.LesmomentValid);
            var result = _controller.StartLesmoment(_context.LesmomentValid.LesmomentId) as RedirectToActionResult;
            Assert.Equal(nameof(_controller.Aanwezigheden), result.ActionName);
        }

        [Fact]
        public void StartLesmoment_AV_alActiefLesmoment_gaatNaarBeheerLesmoment()
        {
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(_context.Lesmomenten);
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns(_context.LesmomentValid);
            var result = _controller.StartLesmoment(_context.LesmomentValid.LesmomentId) as RedirectToActionResult;
            Assert.Equal(nameof(_controller.BeheerLesmoment), result.ActionName);
        }

        [Fact]
        public void StartLesmoment_LesmomentIsNull_gaatNaarError()
        {
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(new List<Lesmoment>() { _context.LesmomentValid });
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns((Lesmoment)null);
            var result = _controller.StartLesmoment(_context.LesmomentValid.LesmomentId) as RedirectToActionResult;
            Assert.Equal(nameof(HomeController.Error), result.ActionName);
        }

        [Fact]
        public void StopLesmoment_NV_Valid_gaatNaarBeheerLesmoment()
        {
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns(_context.LesmomentValid);
            var result = _controller.StopLesmoment(_context.LesmomentValid.LesmomentId) as RedirectToActionResult;
            Assert.Equal(nameof(_controller.BeheerLesmoment), result.ActionName);
        }

        [Fact]
        public void StopLesmoment_LesmomentIsNull_gaatNaarError()
        {
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns((Lesmoment)null);
            var result = _controller.StopLesmoment(_context.LesmomentValid.LesmomentId) as RedirectToActionResult;
            Assert.Equal(nameof(HomeController.Error), result.ActionName);
        }

        #endregion

        #region RegistreerAanwezigheid
        [Fact]
        public void RegistreerAanwezigheid_invalidArguments_Valid()
        {
            _gebruikerRepository.Setup(v => v.GetBy("-1")).Returns((Gebruiker)null);
            _lesmomentRepository.Setup(v => v.GetById(-1)).Returns((Lesmoment)null);
            var result = _controller.RegistreerAanwezigheid(-1, "-1") as RedirectToActionResult;
            Assert.Equal(nameof(HomeController.Error), result.ActionName);
        }

        [Fact]
        public void RegistreerAanwezigheid_validArguments_Valid()
        {
            _gebruikerRepository.Setup(v => v.GetBy(_context.GebruikerInLesmomentLedenVanLesmomentValid.Gebruikersnaam)).Returns(_context.GebruikerInLesmomentLedenVanLesmomentValid);
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns(_context.LesmomentValid);
            var result = _controller.RegistreerAanwezigheid(_context.LesmomentValid.LesmomentId, _context.GebruikerInLesmomentLedenVanLesmomentValid.Gebruikersnaam) as RedirectToActionResult;
            Assert.Equal(nameof(_controller.Aanwezigheden), result.ActionName);
        }

        #endregion

        #region GebruikersPerFormule
        [Fact]
        public void GebruikersPerFormule_invalidArguments_Valid()
        {
            _lesformuleRepository.Setup(v => v.GetById(-1)).Returns((Lesformule)null);
            _lesmomentRepository.Setup(v => v.GetById(-1)).Returns((Lesmoment)null);
            _gebruikerRepository.Setup(v => v.GetAllLedenInFormule((Lesformule)null)).Returns((List<Gebruiker>)null);
            var result = _controller.GebruikersPerFormule(-1, -1) as RedirectToActionResult;
            Assert.Equal(nameof(HomeController.Error), result.ActionName);
        }

        [Fact]
        public void GebruikerPerFormule_validArguments_Valid()
        {
            _lesformuleRepository.Setup(v => v.GetById(_context.Lesformules[0].Id)).Returns(_context.Lesformules[0]);
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns(_context.LesmomentValid);
            _gebruikerRepository.Setup(v => v.GetAllLedenInFormule(_context.Lesformules[0])).Returns(_context.Lijst1Gebruiker);
            var actionResult = _controller.GebruikersPerFormule(_context.LesmomentValid.LesmomentId, _context.Lesformules[0].Id) as ViewResult;
            Assert.IsType<LesmomentGebruikersInFormuleViewModel>(actionResult?.Model);
        }
        #endregion

        #region Aanwezigheden
        [Fact]
        public void Aanwezigheden_LesmomentNogNietGestart_RedirectNaarHomeIndex()
        {
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(new List<Lesmoment>() { _context.LesmomentValid });            
            var actionResult = _controller.Aanwezigheden() as RedirectToActionResult;
            Assert.Equal(nameof(HomeController.Index), actionResult.ActionName);
        }

        [Fact]
        public void Aanwezigheden_validArguments_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(_context.Lesmomenten);
            _gebruikerRepository.Setup(v => v.GetAllLeden()).Returns(_context.Lijst1Gebruiker);
            var actionResult = _controller.Aanwezigheden() as ViewResult;
            Assert.IsType<LesmomentAlgemeenViewModel>(actionResult?.Model);
        }

        #endregion

        
        #region RegistreerAanwezigheidNietIngeschreven
        [Fact]
        public void RegistreerAanwezigheidNietIngeschreven_invalidArguments_RedirectoToAanwezigheden()
        {
            _lesmomentRepository.Setup(v => v.GetById(-1)).Returns((Lesmoment)null);
            var result = _controller.RegistreerAanwezigheidNietIngeschreven(-1) as RedirectToActionResult;
            Assert.Equal(nameof(_controller.Aanwezigheden), result.ActionName);
        }

        [Fact]
        public void RegistreerAanwezigheidNietIngeschreven_validArguments_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns(_context.LesmomentValid);
            _gebruikerRepository.Setup(v => v.GetAllLeden()).Returns(_context.Lijst1Gebruiker);
            var result = _controller.RegistreerAanwezigheidNietIngeschreven(_context.LesmomentValid.LesmomentId) as ViewResult;
            Assert.IsType<LesmomentNietIngeschrevenViewModel>(result?.Model);
        }
        #endregion

        #region RegistreerAanwezigheidProefles
        [Fact]
        public void RegistreerAanwezigheidProefles_Get_ValidArguments_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns(_context.LesmomentValid);
            var result = _controller.RegistreerAanwezigheidProefles(_context.LesmomentValid.LesmomentId) as ViewResult;
            Assert.IsType<LesmomentProeflesViewModel>(result?.Model);
        }

        [Fact]
        public void RegistreerAanwezigheidProefles_Get_InvalidArguments_RedirectToError()
        {
            _lesmomentRepository.Setup(v => v.GetById(-1)).Returns((Lesmoment)null);
            var result = _controller.RegistreerAanwezigheidProefles(-1) as RedirectToActionResult;
            Assert.Equal(nameof(HomeController.Error), result.ActionName);
        }

        [Fact]
        public void RegistreerAanwezigheidProefles_Post_ValidArguments_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(_context.Lesmomenten);
            _gebruikerRepository.Setup(v => v.GetAllLeden()).Returns(_context.Lijst1Gebruiker);
            _lesmomentRepository.Setup(v => v.GetById(_context.LesmomentValid.LesmomentId)).Returns(_context.LesmomentValid);
            LesmomentProeflesViewModel model = new LesmomentProeflesViewModel()
            {
                LesmomentId = _context.LesmomentValid.LesmomentId,
                Naam = "Testing",
                Voornaam = "Testing",
                Email = "test@testing.tt",
                Telefoonnummer = "012345678"
            };
            var result = _controller.RegistreerAanwezigheidProefles(model) as ViewResult;
            Assert.IsType<LesmomentAlgemeenViewModel>(result.Model);
        }

        [Fact]
        public void RegistreerAanwezigheidProefles_Post_InvalidArguments_RedirectToError()
        {
            var result = _controller.RegistreerAanwezigheidProefles(new LesmomentProeflesViewModel()) as RedirectToActionResult;
            Assert.Equal(nameof(HomeController.Error), result.ActionName);
        }
        #endregion
    }
}
