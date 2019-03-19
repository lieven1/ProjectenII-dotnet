using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Taijitan.Controllers;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;
using Taijitan.Models.Domain.IRepositories;
using TaijitanTests.Data;
using Xunit;

namespace TaijitanTests.Controllers {
    public class LesmateriaalControllerTest {
        private readonly LesmateriaalController _controller;
        private readonly DummyDBcontext _context;
        private readonly Mock<ILesmateriaalRepository> _lesmateriaalRepo;
        private readonly Mock<IThemaRepository> _themaRepo;
        private readonly Gebruiker _gebruiker1;

        public LesmateriaalControllerTest() {

            _context = new DummyDBcontext();
            _gebruiker1 = _context.GebruikerInLijst;
            _lesmateriaalRepo = new Mock<ILesmateriaalRepository>();
            _themaRepo = new Mock<IThemaRepository>();
            _controller = new LesmateriaalController(_lesmateriaalRepo.Object, _themaRepo.Object);
        }
        
        [Fact]
        public void Index_GeeftGradenGebruiker() {
            var result = _controller.Index(_gebruiker1.Gradatie.GetHashCode()) as ViewResult;
            var model = (IEnumerable<Gradatie>)result.Model;
            var lijst = Enum.GetValues(typeof(Gradatie)).Cast<Gradatie>().ToList().Where(g => g.CompareTo(_gebruiker1.Gradatie) <= 0);
            Assert.Equal(lijst, model);
            Assert.Contains(_gebruiker1.Gradatie, model);
            Assert.Contains(_gebruiker1.Gradatie - 1, model);
            Assert.DoesNotContain(_gebruiker1.Gradatie + 1, model);
        }

        [Fact]
        public void ToonThemas_GeeftThemasGraad() {
            _themaRepo.Setup(tr => tr.GetAll()).Returns(_context.Themas);
            var result = _controller.ThemaOverzicht((int)Gradatie.GoKyu) as ViewResult;
            var model = (IEnumerable<Thema>)result.Model;
            Assert.Contains(_context.Themas[0], model);
            Assert.DoesNotContain(_context.Themas[1], model);
        }

        [Fact]
        public void ToonLesmateriaal_GeeftLesmateriaalThemaGraad() {
            var thema = _context.Themas[0];
            var graad = Gradatie.GoKyu;
            var lesmateriaal = thema.Lesmateriaal.Where(l => l.Graad.Equals(graad)).ToList();
            _themaRepo.Setup(tr => tr.GetLesmateriaal(thema, graad))
                .Returns(lesmateriaal);
            var result = _controller.LesmateriaalOverzicht(thema, (int)graad) as ViewResult;
            var model = (IEnumerable<Lesmateriaal>)result.Model;
            Assert.Equal(lesmateriaal, model);
        }
    }
}
