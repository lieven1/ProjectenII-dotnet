using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Taijitan.Controllers;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;
using Taijitan.Models.Domain.IRepositories;
using Taijitan.Models.LesmateriaalViewModels;
using TaijitanTests.Data;
using Xunit;

namespace TaijitanTests.Controllers {
    public class LesmateriaalControllerTest {
        private readonly LesmateriaalController _controller;
        private readonly DummyDBcontext _context;
        private readonly Mock<ILesmateriaalRepository> _lesmateriaalRepo;
        private readonly Mock<IThemaRepository> _themaRepo;
        private readonly Mock<IRaadplegingRepository> _raadplegingRepo;
        private readonly Gebruiker _gebruiker1;

        public LesmateriaalControllerTest() {
            _context = new DummyDBcontext();
            _gebruiker1 = _context.GebruikerInLijst;
            _lesmateriaalRepo = new Mock<ILesmateriaalRepository>();
            _themaRepo = new Mock<IThemaRepository>();
            _raadplegingRepo = new Mock<IRaadplegingRepository>();
            _controller = new LesmateriaalController(_lesmateriaalRepo.Object, _themaRepo.Object, _raadplegingRepo.Object);
        }

        #region Overzicht
        [Fact]
        public void Overzicht_geldigeGebruiker_geenFilter_valid() {
            _themaRepo.Setup(tr => tr.GetAll()).Returns(_context.Themas);
            _lesmateriaalRepo.Setup(lr => lr.GetAll()).Returns(_context.Lesmateriaal);
            var result = _controller.Overzicht(_gebruiker1, 0, 0) as ViewResult;
            var model = (IEnumerable<Lesmateriaal>)result.Model;
            var lesmateriaal = _context.Lesmateriaal.Where(l => l.Graad <= _gebruiker1.Gradatie);
            Assert.Equal(lesmateriaal, model);
        }

        [Fact]
        public void Overzicht_geldigeGebruiker_graadEnThemaFilter_valid() {
            int graadInt = (int)Convert.ChangeType(Gradatie.JuniDan, TypeCode.Int32);
            Thema thema = _context.Themas[0];
            _lesmateriaalRepo.Setup(lr => lr.GetAll()).Returns(_context.Lesmateriaal);
            _themaRepo.Setup(tr => tr.GetAll()).Returns(_context.Themas);
            _themaRepo.Setup(tr => tr.GetBy(thema.ThemaId)).Returns(thema);
            var result = _controller.Overzicht(_gebruiker1, graadInt, thema.ThemaId) as ViewResult;
            var model = (IEnumerable<Lesmateriaal>)result.Model;
            var lesmateriaal = _context.Lesmateriaal.Where(l => l.Graad == Gradatie.JuniDan && l.Thema == thema);
            Assert.Equal(lesmateriaal, model);
        }

        [Fact]
        public void Overzicht_ongeldigeGebruiker_invalid_redirectToError() {
            _lesmateriaalRepo.Setup(lr => lr.GetAll()).Returns(_context.Lesmateriaal);
            _themaRepo.Setup(tr => tr.GetAll()).Returns(_context.Themas);
            var result = _controller.Overzicht(null, 0, 0) as RedirectToActionResult;
            Assert.Equal("Error", result.ActionName);
            _lesmateriaalRepo.Verify(r => r.GetAll(), Times.Never);
        }

        [Fact]
        public void Overzicht_ongeldigeGraad_invalid_legeLijst() {
            _lesmateriaalRepo.Setup(lr => lr.GetAll()).Returns(_context.Lesmateriaal);
            _themaRepo.Setup(tr => tr.GetAll()).Returns(_context.Themas);
            var result = _controller.Overzicht(_gebruiker1, -1, 0) as ViewResult;
            var model = (IEnumerable<Lesmateriaal>)result.Model;
            Assert.Empty(model);
        }

        [Fact]
        public void Overzicht_ongeldigeThema_invalid_legeLijst() {
            _lesmateriaalRepo.Setup(lr => lr.GetAll()).Returns(_context.Lesmateriaal);
            _themaRepo.Setup(tr => tr.GetAll()).Returns(_context.Themas);
            var result = _controller.Overzicht(_gebruiker1, 0, -1) as ViewResult;
            var model = (IEnumerable<Lesmateriaal>)result.Model;
            Assert.Empty(model);
        }
        #endregion

        #region details lesmateriaal
        [Fact]
        public void Lesmateriaal_valid() {
            var lesmateriaal = _context.Lesmateriaal[0];
            _lesmateriaalRepo.Setup(r => r.GetById(lesmateriaal.LesmateriaalId)).Returns(lesmateriaal);
            var raadpleging = new Raadpleging(lesmateriaal, _gebruiker1, DateTime.Now);
            _raadplegingRepo.Setup(r => r.AddRaadpleging(raadpleging)).Verifiable();
            _raadplegingRepo.Setup(r => r.SaveChanges()).Verifiable();

            var result = _controller.LesmateriaalMock(_gebruiker1, lesmateriaal.LesmateriaalId, raadpleging) as ViewResult;
            var model = result.Model;

            Assert.Equal(lesmateriaal, model);
            _lesmateriaalRepo.Verify(r => r.GetById(lesmateriaal.LesmateriaalId), Times.Once);
            _raadplegingRepo.Verify(r => r.AddRaadpleging(raadpleging), Times.Once);
            _raadplegingRepo.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Lesmateriaal_ongeldigeGebruiker_invalid_redirectToError() {
            _lesmateriaalRepo.Setup(r => r.GetById(1)).Returns(_context.Lesmateriaal[0]);
            var result = _controller.Lesmateriaal(null, 1) as RedirectToActionResult;
            Assert.Equal("Error", result.ActionName);
        }

        [Fact]
        public void Lesmateriaal_ongeldigLesmateriaal_invalid_returnsNotFound() {
            _lesmateriaalRepo.Setup(r => r.GetById(-1)).Returns((Lesmateriaal)null);
            var result = _controller.Lesmateriaal(_gebruiker1, -1) as NotFoundResult;
            Assert.Equal(404, result.StatusCode);
            _lesmateriaalRepo.Verify(r => r.GetById(-1), Times.Once);
        } 
        #endregion
        
    }
}
